using Newtonsoft.Json;
using ProjectHive.Services.ProjectsAPI;
using ProjectHive.Services.ProjectsAPI.Models;
using ProjectHive.Services.ProjectsAPI.Models.RequestModel;
using System.Net;
using System.Text;

namespace ProjectHive.IntegrationTests
{
    public class ServiceIntegrationTests : IDisposable, IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly TestingWebAppFactory<Program> _server;
        private readonly HttpClient _client;
        public ServiceIntegrationTests(TestingWebAppFactory<Program> server)
        {
            _server = server;
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task GetById_ReturnSuccess()
        {
            var id = "c76e47f1-14eb-4f81-881c-2ceae836fa7e";
            var response = await _client.GetAsync($"/api/Project/GetById/{id}");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteById_ReturnSuccess()
        {
            var id = "c76e47f1-14eb-4f81-881c-2ceae836fa7e";
            var response = await _client.DeleteAsync($"/api/Project/DeleteById/{id}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Create_ReturnSuccess()
        {
            var model = new CreateProjectRequestViewModel
            {
                Name = "Project1289",
                Description = "This is project1289",
                StatusProjectId = Guid.NewGuid(),
                CreatorUserId = Guid.NewGuid()
            };
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/Project/CreateProject", content);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var returnedProject = JsonConvert.DeserializeObject<ProjectViewModel>(await response.Content.ReadAsStringAsync());

            Assert.Equal(model.Name, returnedProject.Name);
            Assert.Equal(model.Description, returnedProject.Description);
        }

        [Fact]
        public async Task Update_ReturnSuccess()
        {
            var model = new UpdateProjectRequestViewModel
            {
                Name = "Project128we9",
                Description = "This is project128wew9",
                StatusProjectId = Guid.NewGuid(),
                CreatorUserId = Guid.NewGuid()
            };
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("/api/Project/UpdateProject", content);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public void Dispose()
        {
            _server.Dispose();
            _client.Dispose();
        }
    }
}