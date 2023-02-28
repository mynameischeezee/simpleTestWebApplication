using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using moviesStorage.IdentityService.Data.Identity;
using moviesStorage.IdentityService.Queries;
using moviesStorage.IdentityService.Responses;

namespace moviesStorage.IdentityService.Handlers;

public class LogoutUserQueryHandler : IRequestHandler<LogoutUserQuery, LogoutUserResponse>
{
    private readonly SignInManager<ServiceUser> _signInManager;
    private readonly IMapper _mapper;

    public LogoutUserQueryHandler(SignInManager<ServiceUser> signInManager, IMapper mapper)
    {
        _signInManager = signInManager;
        _mapper = mapper;
    }

    public async Task<LogoutUserResponse> Handle(LogoutUserQuery request, CancellationToken cancellationToken)
    {
        await _signInManager.SignOutAsync();
        var response = new LogoutUserResponse();
        return response;
    }
}