using System.Net;
using movieStorage.Registration.Responses;
using movieStorage.Registration.ServiceContext;

namespace movieStorage.Registration.IntegrationTests.AuthenticationIntegrationTests;

public class RegistrationIntegrationTests
{
    private HttpClient _httpClient;
    private string _url;
    
    [SetUp]
    public void Setup()
    {
        var webAppFactory = new WebApplicationFactory<RegistrationIntegrationTests>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll<DbContextOptions<IdentityContext>>();
                    services.AddDbContext<IdentityContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                });
            });
        _httpClient = webAppFactory.CreateClient();
        _url = "/register";
    }
    
    [Test]
    public async Task RegisterUser_ShouldReturnUserData_WhenNewUserCreated()
    {
        var testUser = new UserDTO()
        {
            Username = "t3stUs3r",
            Password = "t3$tP@ssw0rd",
            Address = new Address()
            {
                Country = "testCountry",
                CountryCode = "TST",
                Line1 = "testStreet1",
                Line2 = "testStreet2",
                PostCode = "00000",
                Town = "testTown"
            },
            DateOfBirth = DateTime.Today,
            Email = "testMail@testing.com",
            Firstname = "testFirstName",
            Gender = "testGender",
            LastName = "testLastName",
            MiddleName = "testMiddleName",
            PhoneNumber = "+3800000000",
            UserTitle = "Mr.",
            PlaceOfBirth = "testPlaceOfBirth",
        };
        var response = await _httpClient.PostAsJsonAsync(_url, testUser);
        var jsonResult = await response.Content.ReadFromJsonAsync<RegisterUserResponse>();

        var expectedStatusCode = HttpStatusCode.OK;
        var actualStatusCode = response.StatusCode;
        
        var expectedUserMail = "testMail@testing.com";
        var actualUserMail = jsonResult.Email;

        
        Assert.Multiple(() =>
        {
            Assert.That(actualUserMail, Is.EqualTo(expectedUserMail));
            Assert.That(actualStatusCode, Is.EqualTo(expectedStatusCode));
        });
    }

    [Test]
    public async Task RegisterUser_ShouldReturnOk_WhenNewUserCreated()
    {
        var testUser = new UserDTO()
        {
            Username = "UserT3st",
            Password = "t3$tP@ssw0rd",
            Address = new Address()
            {
                Country = "testCountry",
                CountryCode = "TST",
                Line1 = "testStreet1",
                Line2 = "testStreet2",
                PostCode = "00000",
                Town = "testTown"
            },
            DateOfBirth = DateTime.Today,
            Email = "testMail@gmail.com",
            Firstname = "testFirstName",
            Gender = "testGender",
            LastName = "testLastName",
            MiddleName = "testMiddleName",
            PhoneNumber = "+3800000000",
            UserTitle = "Mr.",
            PlaceOfBirth = "testPlaceOfBirth",
        };
        var response = await _httpClient.PostAsJsonAsync(_url, testUser);
        
        var expectedStatusCode = HttpStatusCode.OK;
        var actualStatusCode = response.StatusCode;

        Assert.That(actualStatusCode, Is.EqualTo(expectedStatusCode));
    }

    [Test]
    public async Task RegisterUser_ShouldReturnBadRequest_WhenAddEmptyUser()
    {
        var testUser = new UserDTO();
        var response = await _httpClient.PostAsJsonAsync(_url, testUser);
        
        var expectedStatusCode = HttpStatusCode.BadRequest;
        var actualStatusCode = response.StatusCode;

        Assert.That(actualStatusCode, Is.EqualTo(expectedStatusCode));
    }
    
    [Test]
    public async Task RegisterUser_ShouldReturnBadRequest_WhenAddTwoIdenticalUsers()
    {
        var testUser = new UserDTO()
        {
            Username = "testUser",
            Password = "t3$tP@ssw0rd",
            Address = new Address()
            {
                Country = "testCountry",
                CountryCode = "TST",
                Line1 = "testStreet1",
                Line2 = "testStreet2",
                PostCode = "00000",
                Town = "testTown"
            },
            DateOfBirth = DateTime.Today,
            Email = "testMail@test.com",
            Firstname = "testFirstName",
            Gender = "testGender",
            LastName = "testLastName",
            MiddleName = "testMiddleName",
            PhoneNumber = "+3800000000",
            UserTitle = "Mr.",
            PlaceOfBirth = "testPlaceOfBirth",
        };
        await _httpClient.PostAsJsonAsync(_url, testUser);
        var response = await _httpClient.PostAsJsonAsync(_url, testUser);
            
        var expectedStatusCode = HttpStatusCode.BadRequest;
        var actualStatusCode = response.StatusCode;

        Assert.That(actualStatusCode, Is.EqualTo(expectedStatusCode));

        Assert.That(actualStatusCode, Is.EqualTo(expectedStatusCode));
    }

    [Test]
    public async Task RegisterUser_ShouldReturnBadRequest_WhenPassDataInRegisterUrl()
    {
        var testUser = new UserDTO();
        var testUrl = _url + 
                      "?userTitle=testUser" +
                      "&username=test&firstname=test" +
                      "&lastName=test&middleName=test&email=user@example.com" +
                      "&placeOfBirth=test&dateOfBirth=2023-01-05T11:15:46.841Z" +
                      "&gender=test" +
                      "&password=t3$tP@ssw0rd" +
                      "&phoneNumber=string" +
                      "&address[line1]=test" +
                      "&address[line2]=test" +
                      "&address[town]=test" +
                      "&address[country]=test" +
                      "&address[postCode]=test" +
                      "&address[countryCode]=test";
        var response = await _httpClient.PostAsJsonAsync(testUrl, testUser);
        
        var expectedStatusCode = HttpStatusCode.BadRequest;
        var actualStatusCode = response.StatusCode;

        Assert.That(actualStatusCode, Is.EqualTo(expectedStatusCode));
    }
}