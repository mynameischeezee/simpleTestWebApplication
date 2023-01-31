using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using moviesStorage.IdentityService.Commands;
using moviesStorage.IdentityService.Data.Identity;
using moviesStorage.IdentityService.Exceptions.LoginExceptions;
using moviesStorage.IdentityService.Responses;
using moviesStorage.IdentityService.ServiceContext;

namespace movieStorage.Registration.Handlers;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly SignInManager<ServiceUser> _signInManager;
    private readonly IMapper _mapper;

    public LoginUserCommandHandler(SignInManager<ServiceUser> signInManager, IMapper mapper)
    {
        _signInManager = signInManager;
        _mapper = mapper;
    }

    public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Username,
            request.Password,
            isPersistent: false,
            lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            throw new WrongCredentialsException();
        }

        var response = new LoginUserResponse();
        return response;
    }
}