using MediatR;
using movieStorage.Identity.Models;
using movieStorage.Identity.Responses;

namespace movieStorage.Identity.Commands;
public class RegisterUserCommand : IRequest<RegisterUserResponse>
{
    public UserDTO UserDto { get; set; }

    public RegisterUserCommand(UserDTO userDto)
    {
        UserDto = userDto;
    }
}