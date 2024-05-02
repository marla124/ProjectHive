using Newtonsoft.Json;
using ProjectHive.Services.AuthAPI.Models;
using System.Net;
using System.Text;

namespace ProjectHive.IntegrationTestsForAuth;

public class TokenControllerIntegrationTests : BaseIntegrationTestForAuth
{
    private const string BaseUrl = "/api/Token";

    [Fact]
    public async Task GenerateToken_ReturnSuccess()
    {
        var user = await PopulateUserToDatabase();
        var model = new LoginModel
        {
            Email = user.Email,
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
        var token = await PopulateTokenToDatabase();
        var model = new RefreshTokenModel
        {
            RefreshToken = token.Id,
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
        var model = new RefreshTokenModel()
        {
            RefreshToken = token.Id
        };
        var uri = $"{BaseUrl}/Revoke/{model.RefreshToken}";
        var response = await _httpClient.DeleteAsync(uri);
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}