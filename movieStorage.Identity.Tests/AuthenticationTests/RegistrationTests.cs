using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using movieStorage.Identity.Commands;
using movieStorage.Identity.Data;
using movieStorage.Identity.Data.Identity;
using movieStorage.Identity.Handlers;
using movieStorage.Identity.Models;
using movieStorage.Identity.Responses;
using movieStorage.Identity.Tests.AuthenticationTests.Mocks;

namespace movieStorage.Identity.Tests.AuthenticationTests;

public class RegistrationTests
{
    private UserManager<ServiceUser> _userManager;
    private IMapper _mapper;
    private IMediator _mediator;
    private List<ServiceUser> _users;

    [SetUp]
    public void Setup()
    {
        _users = new List<ServiceUser>()
        {
            new ServiceUser()
        };
        _userManager = new FakeUserManager<ServiceUser>(_users).Create();
        _mapper = new FakeAutoMapper().Create();
        _mediator = new  Mock<IMediator>().Object;
    }

    [Test]
    public void RegisterNewUser_ShouldFail_WhenAddEmptyUser()
    {
        var testUser = new UserDTO() {};
        var registerUserRequestCommand = new RegisterUserCommand(testUser);
        var registerUserCommandHandler = new RegisterUserCommandHandler(_mapper, _userManager);

        var actual = _users.Count;
        var expected = 1;
        
        Assert.ThrowsAsync<RegistrationFailedException>(async () =>
            await registerUserCommandHandler.Handle(registerUserRequestCommand, new CancellationToken()));
        Assert.That(actual, Is.EqualTo(expected));
        
    }
    
    [Test]
    public void RegisterNewUser_ShouldFail_WhenAddUserWithEmptyPassword()
    {
        var testUser = new UserDTO()
        {
            Username = "testUser",
            Password = null,
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
        var registerUserCommand = new RegisterUserCommand(testUser);
        var registerUserCommandHandler = new RegisterUserCommandHandler(_mapper, _userManager);
        
        var actual = _users.Count;
        var expected = 1;
        
        Assert.ThrowsAsync<RegistrationFailedException>(async () =>
            await registerUserCommandHandler.Handle(registerUserCommand, new CancellationToken()));
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public async Task UsersList_ShouldBeTwo_WhenAddUserWithValidatedCredentials()
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
        var registerUserCommand = new RegisterUserCommand(testUser);
        var registerUserCommandHandler = new RegisterUserCommandHandler(_mapper, _userManager);
        await registerUserCommandHandler.Handle(registerUserCommand, new CancellationToken());

        var actual = _users.Count;
        var expected = 2;
        
        Assert.That(actual, Is.EqualTo(expected));
    }
}