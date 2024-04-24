using Newtonsoft.Json;
using ProjectHive.Services.AuthAPI.Models;
using System.Net;
using System.Text;

namespace ProjectHive.IntegrationTests;

public class TokenControllerIntegrationTests : BaseIntegrationTest
{
    private const string BaseUrl = "/api/Token";

    [Fact]
    public async Task GenerateToken_ReturnSuccess()
    {
        var model = new LoginModel
        {
            Email = "testemail2@gmail.com",
            Password = "password"
        };
        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        var uri = $"{BaseUrl}/GenerateToken";
        var response = await _httpClient.PostAsync(uri, content);

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Refresh_ReturnSuccess()
    {
        var model = new RefreshTokenModel
        {
            RefreshToken = Guid.NewGuid(),
        };
        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        var uri = $"{BaseUrl}/Refresh";
        var response = await _httpClient.PostAsync(uri, content);

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task DeleteById_ReturnSuccess()
    {
        var token = await PopulateTokenToDatabase();

        var response = await _httpClient.DeleteAsync($"{BaseUrl}/Revoke/{token.Id}");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}