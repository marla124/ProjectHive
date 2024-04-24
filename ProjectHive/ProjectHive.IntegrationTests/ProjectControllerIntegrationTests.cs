using Newtonsoft.Json;
using ProjectHive.Services.ProjectsAPI.Models;
using ProjectHive.Services.ProjectsAPI.Models.RequestModel;
using System.Net;
using System.Text;

namespace ProjectHive.IntegrationTests;

public class ProjectControllerIntegrationTests : BaseIntegrationTest
{
    private const string BaseUrl = "/api/Project";

    [Fact]
    public async Task GetById_ProjectExists_ReturnSuccess()
    {
        var project = await PopulateProgectToDatabase();

        var response = await _httpClient.GetAsync($"{BaseUrl}/GetById/{project.Id}");

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetById_ProjectNotExists_ReturnNotFound()
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/GetById/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteById_ReturnSuccess()
    {
        var project = await PopulateProgectToDatabase();

        var response = await _httpClient.DeleteAsync($"{BaseUrl}/DeleteById/{project.Id}");
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
        var uri = $"{BaseUrl}/CreateProject";
        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(uri, content);

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var returnedProject = JsonConvert.DeserializeObject<ProjectViewModel>(await response.Content.ReadAsStringAsync());

        Assert.Equal(model.Name, returnedProject!.Name);
        Assert.Equal(model.Description, returnedProject.Description);
    }

    [Fact]
    public async Task Update_ReturnSuccess()
    {
        var project = await PopulateProgectToDatabase();

        var model = new UpdateProjectRequestViewModel
        {
            Id = project.Id,
            Name = "TestProject1",
            Description = "This is test project update",
            StatusProjectId = Guid.NewGuid(),
            CreatorUserId = Guid.NewGuid()
        };
        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        var response = await _httpClient.PatchAsync($"{BaseUrl}/UpdateProject", content);

        response.EnsureSuccessStatusCode();
        var returnedProject = JsonConvert.DeserializeObject<ProjectViewModel>(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Assert.Equal(model.Id, returnedProject!.Id);
        Assert.Equal(model.Name, returnedProject.Name);
        Assert.Equal(model.Description, returnedProject.Description);
    }
}