using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using movieStorage.Identity.Commands;
using movieStorage.Identity.Data.Identity;
using movieStorage.Identity.Exceptions;
using movieStorage.Identity.Responses;

namespace movieStorage.Identity.Handlers;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IMapper _mapper;
    private readonly UserManager<ServiceUser> _userManager;

    public RegisterUserCommandHandler(IMapper mapper, UserManager<ServiceUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<ServiceUser>(request.UserDto);
        var result = await _userManager.CreateAsync(user, request.UserDto.Password);
        if (!result.Succeeded)
        {
            throw new RegistrationFailedException(result.Errors);
        }
        var response = _mapper.Map<RegisterUserResponse>(user);
        return response;
    }
}