using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using moviesStorage.IdentityService.Commands;
using moviesStorage.IdentityService.Data.Identity;
using moviesStorage.IdentityService.Exceptions.LoginExceptions;
using moviesStorage.IdentityService.Responses;
using Microsoft.AspNetCore.Authentication;

namespace moviesStorage.IdentityService.Handlers;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly UserManager<ServiceUser> _userManager;
    private readonly SignInManager<ServiceUser> _signInManager;
    private readonly IMapper _mapper;

    public LoginUserCommandHandler(SignInManager<ServiceUser> signInManager, IMapper mapper, UserManager<ServiceUser> userManager)
    {
        _signInManager = signInManager;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == default)
        {
            throw new UserNotExistException(request.Username);
        }

        var loginResult = await _signInManager.PasswordSignInAsync(request.Username,
            request.Password,
            isPersistent: false,
            lockoutOnFailure: false);

        if (!loginResult.Succeeded)
        {
            throw new WrongCredentialsException();
        }

        var response = new LoginUserResponse()
        {
            
        };
        return response;
    }
}