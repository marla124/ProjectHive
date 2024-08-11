using Newtonsoft.Json;
using ProjectHive.Services.ProjectsAPI.Models;
using ProjectHive.Services.ProjectsAPI.Models.RequestModel;
using System.Net;
using System.Text;

namespace ProjectHive.ProjectAPI.IntegrationTests;

public class ProjectTasksControllerIntegrationTests : BaseIntegrationTest
{
    private const string BaseUrl = "/api/ProjectTask";

    [Fact]
    public async Task Create_ReturnSuccess()
    {
        //var project = await PopulateProgectToDatabaseProject();
        //var task = await PopulateProgectToDatabaseTask();
        ////var model = new CreateTaskRequestViewModel
        ////{
        ////    Name = "name",
        ////    Description = "description",
        ////    Deadline = DateTime.Now,
        ////    ProjectId=project.Id,
        ////};
        //var uri = $"{BaseUrl}/CreateTask";
        //var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        //var response = await _httpClient.PostAsync(uri, content);

        //response.EnsureSuccessStatusCode();
        //Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //var returnedProject = JsonConvert.DeserializeObject<ProjectViewModel>(await response.Content.ReadAsStringAsync());

        //Assert.Equal(model.Name, returnedProject!.Name);
        //Assert.Equal(model.Description, returnedProject.Description);
    }

    [Fact]
    public async Task GetById_ProjectExists_ReturnSuccess()
    {
        var task = await PopulateProgectToDatabaseTask();

        var response = await _httpClient.GetAsync($"{BaseUrl}/GetById/{task.Id}");

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}