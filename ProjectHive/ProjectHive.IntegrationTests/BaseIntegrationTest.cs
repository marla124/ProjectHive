using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectHive.Services.AuthAPI.Data;
using ProjectHive.Services.AuthAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI;
using ProjectHive.Services.ProjectsAPI.Data;
using ProjectHive.Services.ProjectsAPI.Data.Entities;

namespace ProjectHive.IntegrationTests;

public class BaseIntegrationTest : IDisposable
{
    private readonly ProjectHiveProjectDbContext? _dbContextForProject;
    private readonly ProjectHiveAuthDbContext? _dbContextForAuth;

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

                var descriptorAuth = services.SingleOrDefault(d => d.ServiceType ==
                        typeof(DbContextOptions<ProjectHiveAuthDbContext>));
                if (descriptorAuth != null)
                    services.Remove(descriptorAuth);
                services.AddDbContext<ProjectHiveAuthDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryAuthTest");
                });
            });
        });

        var serviceProvider = _webApplicationFactory.Services.CreateScope().ServiceProvider;

        _httpClient = _webApplicationFactory.CreateClient();
        _dbContextForProject = serviceProvider.GetService<ProjectHiveProjectDbContext>()!;
        _dbContextForProject?.Database.EnsureCreated();

        _dbContextForAuth = serviceProvider.GetService<ProjectHiveAuthDbContext>()!;
        _dbContextForAuth?.Database.EnsureCreated();
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

        _dbContextForProject!.Projects.Add(project);
        await _dbContextForProject.SaveChangesAsync();

        return project;
    }
    protected async Task<RefreshToken> PopulateTokenToDatabase()
    {
        var token = new RefreshToken
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            ExpiringAt = DateTime.Now,
            AssociateDeviceName = "name",
            UserId = Guid.NewGuid()
        };

        _dbContextForAuth!.RefreshTokens.Add(token);
        await _dbContextForAuth.SaveChangesAsync();

        return token;
    }

    public void Dispose()
    {
        _dbContextForProject?.Database.EnsureDeleted();
        _dbContextForProject?.Dispose();
        _dbContextForAuth?.Database.EnsureDeleted();
        _dbContextForAuth?.Dispose();
        _httpClient.Dispose();

        GC.SuppressFinalize(this);
    }
}
