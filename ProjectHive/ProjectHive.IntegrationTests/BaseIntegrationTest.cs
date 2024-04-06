using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectHive.Services.ProjectsAPI;
using ProjectHive.Services.ProjectsAPI.Data;
using ProjectHive.Services.ProjectsAPI.Data.Entities;

namespace ProjectHive.IntegrationTests;

public class BaseIntegrationTest : IDisposable
{
    private readonly ProjectHiveProjectDbContext? _dbContext;
    protected readonly HttpClient _httpClient;
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public BaseIntegrationTest()
    {
        _webApplicationFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType ==
                        typeof(DbContextOptions<ProjectHiveProjectDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<ProjectHiveProjectDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryProjectTest");
                });
            });
        });

        var serviceProvider = _webApplicationFactory.Services.CreateScope().ServiceProvider;

        _httpClient = _webApplicationFactory.CreateClient();
        _dbContext = serviceProvider.GetService<ProjectHiveProjectDbContext>()!;
        _dbContext?.Database.EnsureCreated();
    }

    protected async Task<Project> PopulateProgectToDatabase()
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

        _dbContext!.Projects.Add(project);
        await _dbContext.SaveChangesAsync();

        return project;
    }

    public void Dispose()
    {
        _dbContext?.Database.EnsureDeleted();
        _dbContext?.Dispose();
        _httpClient.Dispose();

        GC.SuppressFinalize(this);
    }
}
