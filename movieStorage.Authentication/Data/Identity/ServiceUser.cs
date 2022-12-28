using Microsoft.AspNetCore.Identity;
using movieStorage.Authentication.Data.Enumerations;

namespace movieStorage.Authentication.Data.Identity;

public class ServiceUser : IdentityUser
{
    public Title UserTitle { get; set; }

    public string Username { get; set; }

    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public string PlaceOfBirth { get; set; }

    public DateTime DateOfBirth { get; set; }
    
    public Gender Gender { get; set; }

    public string Password { get; set; }

    public string CountryCode { get; set; }

    public string PostCode { get; set; }
}