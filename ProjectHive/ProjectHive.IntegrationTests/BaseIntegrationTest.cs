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
        var project = new Project
        {
            Name = "TestProject1",
            Description = "This is test project",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            CreatorUserId = Guid.NewGuid(),
            StatusProjectId = Guid.NewGuid(),
        };

        _dbContextForProject!.Projects.Add(project);
        await _dbContextForProject.SaveChangesAsync();

        return project;
    }

    public void Dispose()
    {
        _dbContextForProject?.Database.EnsureDeleted();
        _dbContextForProject?.Dispose();
        _httpClient.Dispose();

        GC.SuppressFinalize(this);
    }
}
