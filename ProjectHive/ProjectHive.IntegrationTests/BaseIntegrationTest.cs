using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectHive.Services.ProjectsAPI;
using ProjectHive.Services.ProjectsAPI.Data;
using ProjectHive.Services.ProjectsAPI.Data.Entities;

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
        _dbContextForProject = serviceProvider.GetService<ProjectHiveProjectDbContext>()!;
        _dbContextForProject?.Database.EnsureCreated();
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
