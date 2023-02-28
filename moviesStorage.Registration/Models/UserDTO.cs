using System.ComponentModel.DataAnnotations;
using moviesStorage.Registration.Data;
using moviesStorage.Registration.DataValidationAttributes;

namespace moviesStorage.Registration.Models;

public class UserDTO
{
    public string UserTitle { get; set; }

    [Required]
    [NameData(maximumLength: 20)]
    public string Username { get; set; }

    [Required]
    [NameData(maximumLength: 20)]
    public string Firstname { get; set; }

    [Required]
    [NameData(maximumLength: 20)]
    public string LastName { get; set; }

    [NameData(maximumLength: 20)]
    public string MiddleName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    public string PlaceOfBirth { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime DateOfBirth { get; set; }
    
    [Required]
    public string Gender { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(8)]
    public string Password { get; init; }

    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } 
    
    [Required]
    public Address Address { get; init; }

    public UserStatus Status { get; set; } = UserStatus.Active;
}