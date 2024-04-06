using Newtonsoft.Json;
using ProjectHive.Services.ProjectsAPI;
using ProjectHive.Services.ProjectsAPI.Models;
using ProjectHive.Services.ProjectsAPI.Models.RequestModel;
using System.Net;
using System.Text;

namespace ProjectHive.IntegrationTests
{
    public class ProjectControllerIntegrationTests : IDisposable, IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly TestingWebAppFactory<Program> _server;
        private readonly HttpClient _client;
        private const string ProjectId = "c76e47f1-14eb-4f81-881c-2ceae836fa7e";
        private const string BaseUrl = "/api/Project/";
        public ProjectControllerIntegrationTests(TestingWebAppFactory<Program> server)
        {
            _server = server;
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task GetById_ReturnSuccess()
        {
            var response = await _client.GetAsync($"{BaseUrl}GetById/{ProjectId}");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteById_ReturnSuccess()
        {
            var response = await _client.DeleteAsync($"{BaseUrl}DeleteById/{ProjectId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Create_ReturnSuccess()
        {
            var model = new CreateProjectRequestViewModel
            {
                Id = Guid.Parse(ProjectId),
                Name = "Project1289",
                Description = "This is project1289",
                StatusProjectId = Guid.NewGuid(),
                CreatorUserId = Guid.NewGuid()
            };
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{BaseUrl}CreateProject", content);

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
                Id = Guid.Parse(ProjectId),
                Name = "TestProject1",
                Description = "This is test project update",
                StatusProjectId = Guid.NewGuid(),
                CreatorUserId = Guid.NewGuid()
            };
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _client.PatchAsync($"{BaseUrl}UpdateProject", content);

            response.EnsureSuccessStatusCode();
            var returnedProject = JsonConvert.DeserializeObject<ProjectViewModel>(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.Equal(model.Name, returnedProject.Name);
            Assert.Equal(model.Description, returnedProject.Description);
        }

        public void Dispose()
        {
            _server.Dispose();
            _client.Dispose();
        }
    }
}