using MediatR;
using movieStorage.Registration.Models;
using movieStorage.Registration.Responses;

namespace movieStorage.Registration.Commands;
public class RegisterUserCommand : IRequest<RegisterUserResponse>
{
    public UserDTO UserDto { get; set; }

    public RegisterUserCommand(UserDTO userDto)
    {
        UserDto = userDto;
    }
}