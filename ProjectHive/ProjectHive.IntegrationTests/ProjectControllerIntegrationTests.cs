using ProjectHive.Services.ProjectsAPI;

namespace ProjectHive.IntegrationTests
{
    public class ProjectControllerIntegrationTests : IDisposable, IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly TestingWebAppFactory<Program> _server;
        private readonly HttpClient _client;
        public ProjectControllerIntegrationTests(TestingWebAppFactory<Program> server)
        {
            _server = server;
            _client = _server.CreateClient();
        }
        public void Dispose()
        {
            _server.Dispose();
            _client.Dispose();
        }
        [Fact]
        public async Task GetById_ReturnSuccess()
        {
            var id = "c76e47f1-14eb-4f81-881c-2ceae836fa7e";
            var response = await _client.GetAsync($"/api/Project/{id}");
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task DeleteById_ReturnSuccess()
        {
            var id = "c76e47f1-14eb-4f81-881c-2ceae836fa7e";
            var response = await _client.DeleteAsync($"/api/Project/{id}");
            response.EnsureSuccessStatusCode();
        }

    }
}