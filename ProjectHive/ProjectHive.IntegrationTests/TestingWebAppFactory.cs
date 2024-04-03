using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectHive.Services.ProjectsAPI;
using ProjectHive.Services.ProjectsAPI.Data;
using ProjectHive.Services.ProjectsAPI.Data.Entities;

namespace ProjectHive.IntegrationTests
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ProjectHiveProjectDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);
                services.AddDbContext<ProjectHiveProjectDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryProjectTest");
                });
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<ProjectHiveProjectDbContext>())
                {
                    try
                    {
                        appContext.Database.EnsureCreated();
                        var testProject1 = new Project
                        {
                            Id = Guid.Parse("c76e47f1-14eb-4f81-881c-2ceae836fa7e"),
                            Name = "TestProject1",
                            Description = "This is test project",
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            CreatorUserId = Guid.NewGuid(),
                            StatusProjectId = Guid.NewGuid(),
                        };
                        appContext.Projects.Add(testProject1);
                        appContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            });
        }
    }
}
