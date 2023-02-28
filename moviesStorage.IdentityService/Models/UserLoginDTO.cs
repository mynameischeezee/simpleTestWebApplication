using System.ComponentModel.DataAnnotations;

namespace moviesStorage.IdentityService.Models;

public class UserLoginDTO
{
    [Required]
    [StringLength(maximumLength: 20,
        ErrorMessage = "User name must be from {2} to {1} characters long",
        MinimumLength = 3)]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(8)]
    public string Password { get; set; }
}