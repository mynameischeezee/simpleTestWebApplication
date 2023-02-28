using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using moviesStorage.IdentityService.Data.Identity;
using moviesStorage.IdentityService.Models;
using moviesStorage.IdentityService.Repository.Abstraction;
using moviesStorage.IdentityService.ServiceContext;

namespace movieStorage.IdentityService.IntegrationTests.IdentityTests;

public class LogoutIntegrationTests
{
    private HttpClient _httpClient;
    private const string LogoutUrl = "/logout";
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
    public async Task LogoutUser_ShouldFailWithStatusCode405_WhenTryingToLogoutWithoutLoginSession()
    {
        var response = await _httpClient.GetAsync(LogoutUrl);

        const HttpStatusCode expectedStatusCode = HttpStatusCode.MethodNotAllowed;
        Assert.That(response.StatusCode, Is.EqualTo(expectedStatusCode));
    }
    
    [Test]
    public async Task LogoutUser_ShouldReturnStatusCode200_WhenTryingToLogoutWithLoginSession()
    {
        var userCredentials = new UserLoginDTO()
        {
            Username = "testUser",
            Password = "P@ssw0rD123"
        };
        var jsonContent = JsonContent.Create(userCredentials);

        await _httpClient.PostAsync(
            requestUri: LoginUrl,
            content: jsonContent);
        
        var response = await _httpClient.GetAsync(LogoutUrl);
        
        const HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
        Assert.That(response.StatusCode, Is.EqualTo(expectedStatusCode));
    }
    
    [Test]
    public async Task LogoutUser_ShouldFailWithStatusCode405_WhenTryingToLogoutAfterSessionExpired()
    {
        var userCredentials = new UserLoginDTO()
        {
            Username = "testUser",
            Password = "P@ssw0rD123"
        };
        var jsonContent = JsonContent.Create(userCredentials);

        await _httpClient.PostAsync(
            requestUri: LoginUrl,
            content: jsonContent);
        
        const int fourSecondsDelayTime = 4000;
        Thread.Sleep(fourSecondsDelayTime);
        
        var response = await _httpClient.GetAsync(LogoutUrl);
        const HttpStatusCode expectedStatusCode = HttpStatusCode.MethodNotAllowed;
        Assert.That(response.StatusCode, Is.EqualTo(expectedStatusCode));
    }
}