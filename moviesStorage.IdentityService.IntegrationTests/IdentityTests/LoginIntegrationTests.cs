using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using moviesStorage.IdentityService.Data.Identity;
using moviesStorage.IdentityService.Models;
using moviesStorage.IdentityService.Repository.Abstraction;
using moviesStorage.IdentityService.ServiceContext;


namespace movieStorage.IdentityService.IntegrationTests.IdentityTests;

public class LoginIntegrationTests
{
    private HttpClient _httpClient;
    private const string LoginUrl = "/login";
    
    [SetUp]
    public void Setup()
    {
        var webAppFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll<DbContextOptions<IdentityContext>>();
                    services.AddDbContext<IdentityContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                });
            });
        using var scope = webAppFactory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IdentityContext>();
        db.SeedTestUser();
        _httpClient = webAppFactory.CreateClient();
    }
    
    [Test]
    public async Task LoginUser_ShouldFailWithStatusCode415_WhenSendingCredentialsAsText()
    {
        var userCredentials = new UserLoginDTO()
        {
            Username = "unsupportedTypeName",
            Password = "unsupportedTypePassword"
        };
        var stringContent = new StringContent(JsonSerializer.Serialize(value: userCredentials));
        
        var response = await _httpClient.PostAsync(
            requestUri: LoginUrl,
            content: stringContent);
        
        var actualStatusCode = response.StatusCode;

        
        //malo prosto porivnuvati status codi
        Assert.That(actualStatusCode, Is.EqualTo(HttpStatusCode.UnsupportedMediaType));
        
        Assert.AreEqual(HttpStatusCode.UnsupportedMediaType, actualStatusCode);
    }
    
    [Test]
    public async Task LoginUser_ShouldFailWithStatusCode400_WhenUserCredentialsAreWrong()
    {
        var userCredentials = new UserLoginDTO()
        {
            Username = "wrongUserName",
            Password = "wrongTestPassword"
        };
        var jsonContent = JsonContent.Create(userCredentials);

        var response = await _httpClient.PostAsync(
            requestUri: LoginUrl,
            content: jsonContent);
        
        var actualStatusCode = response.StatusCode;
        const HttpStatusCode expectedStatusCode = HttpStatusCode.BadRequest;
        
        //create an error msg
        //assert error msgs
        Assert.That(actualStatusCode, Is.EqualTo(expectedStatusCode));
    }
    
    [Test]
    public async Task LoginUser_ShouldReturnStatusCode200_WhenUserCredentialsAreRight()
    {
        // Register User Here
        // var cl = new HttpClient();
        // var response = cl.PostAsync("localhost:5001/register")
        
        
        
        var userCredentials = new UserLoginDTO()
        {
            Username = "testUser",
            Password = "P@ssw0rD123"
        };
        var jsonContent = JsonContent.Create(userCredentials);

        var response = await _httpClient.PostAsync(
            requestUri: LoginUrl,
            content: jsonContent);
        
        var actualStatusCode = response.StatusCode;
        const HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
        
        
        Assert.That(actualStatusCode, Is.EqualTo(expectedStatusCode));
    }
}