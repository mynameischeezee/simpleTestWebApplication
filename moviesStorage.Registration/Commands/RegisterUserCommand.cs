using MediatR;
using moviesStorage.Registration.Models;
using moviesStorage.Registration.Responses;

namespace moviesStorage.Registration.Commands;
public class RegisterUserCommand : IRequest<RegisterUserResponse>
{
    public UserDTO UserDto { get; set; }

    public RegisterUserCommand(UserDTO userDto)
    {
        UserDto = userDto;
    }
}