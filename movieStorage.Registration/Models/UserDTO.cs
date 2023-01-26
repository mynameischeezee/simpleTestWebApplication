using System.ComponentModel.DataAnnotations;
using movieStorage.Registration.Data;

namespace movieStorage.Registration.Models;

public class UserDTO
{
    public string UserTitle { get; set; }

    [Required]
    [StringLength(maximumLength: 20,
        ErrorMessage = "User name must be from {2} to {1} characters long",
        MinimumLength = 3)]
    public string Username { get; set; }

    [Required]
    [StringLength(maximumLength: 20,
        ErrorMessage = "First name must be from {2} to {1} characters long",
        MinimumLength = 3)]
    public string Firstname { get; set; }

    [Required]
    [StringLength(maximumLength: 20,
        ErrorMessage = "Last name must be from {2} to {1} characters long",
        MinimumLength = 3)]
    public string LastName { get; set; }

    [StringLength(maximumLength: 20,
        ErrorMessage = "Middle name must be from {2} to {1} characters long",
        MinimumLength = 3)]
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
    public string Password { get; set; }

    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } 
    
    [Required]
    public Address Address { get; set; }
}