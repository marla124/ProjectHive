using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ProjectHive.Services.ProjectsAPI;
using ProjectHive.Services.ProjectsAPI.Data;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace ProjectHive.ProjectAPI.IntegrationTests;

public class BaseIntegrationTest : IDisposable
{
    private readonly ProjectHiveProjectDbContext? _dbContextForProject;
    protected readonly HttpClient _httpClient;
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public BaseIntegrationTest()
    {
        _webApplicationFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptorProject = services.SingleOrDefault(d => d.ServiceType ==
                        typeof(DbContextOptions<ProjectHiveProjectDbContext>));
                if (descriptorProject != null)
                    services.Remove(descriptorProject);
                services.AddDbContext<ProjectHiveProjectDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryProjectTest");
                });
            });
        });

        var serviceProvider = _webApplicationFactory.Services.CreateScope().ServiceProvider;
        _httpClient = _webApplicationFactory.CreateClient();
        var fakeUserJwt = CreateJwtForFakeUser();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", fakeUserJwt);

        _dbContextForProject = serviceProvider.GetService<ProjectHiveProjectDbContext>()!;
        _dbContextForProject?.Database.EnsureCreated();
    }

    private string CreateJwtForFakeUser()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("EB622E6F-F21D-44ED-9798-D993A8126605"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Sub, "UserTest"),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("userId", "1DFFFC38-0530-4E64-A1F7-1904A5A72017"),
        new Claim(ClaimTypes.Role, "Admin")
        };

        var token = new JwtSecurityToken(
            issuer: "ProjectHive",
            audience: "ProjectHive",
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    protected async Task<Project> PopulateProgectToDatabaseProject()
    {
        var statusList = new List<ProjectStatus>
        {
        new ProjectStatus { Name = "Processing" },
        new ProjectStatus { Name = "Distributing" },
        new ProjectStatus { Name = "Abandoned" }
        };

        await _dbContextForProject.ProjectStatuses.AddRangeAsync(statusList);
        var processingStatus = statusList.First(s => s.Name == "Processing");

        var user = new User()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        _dbContextForProject.Users.Add(user);

        var project = new Project
        {
            Name = "TestProject1",
            Description = "This is test project",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            CreatorUserId = user.Id,
            StatusProjectId = processingStatus.Id,
        };
        _dbContextForProject!.Projects.Add(project);

        await _dbContextForProject.SaveChangesAsync();

        return project;
    }

    protected async Task<ProjectTask> PopulateProgectToDatabaseTask()
    {
        var statusList = new List<StatusTasks>
        {
           new StatusTasks { Name = "Open" },
           new StatusTasks { Name = "In Progress" },
           new StatusTasks { Name = "Done" },
           new StatusTasks { Name = "Cancelled" },
        };

        await _dbContextForProject.TasksStatuses.AddRangeAsync(statusList);

        var openStatus = statusList.First(s => s.Name == "Open");
        var project = await PopulateProgectToDatabaseProject();
        var projectTask = new ProjectTask
        {
            Name = "TestProjectTask1",
            Description = "This is test projectTask",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Deadline = DateTime.Now,
            ProjectId = project.Id,
            Id = Guid.NewGuid(),
            StatusTaskId = openStatus.Id
        };
        _dbContextForProject!.ProjectTasks.Add(projectTask);
        await _dbContextForProject.SaveChangesAsync();

        return projectTask;
    }
    public void Dispose()
    {
        _dbContextForProject?.Database.EnsureDeleted();
        _dbContextForProject?.Dispose();
        _httpClient.Dispose();

        GC.SuppressFinalize(this);
    }
}
