﻿using Microsoft.AspNetCore.Identity;

namespace movieStorage.Identity.Data.Identity;

public class ServiceUser : IdentityUser
{
    public string UserTitle { get; set; }

    public string Firstname { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public string PlaceOfBirth { get; set; }

    public DateTime DateOfBirth { get; set; }
    
    public string Gender { get; set; }

    public string CountryCode { get; set; }

    public string PostCode { get; set; }
}