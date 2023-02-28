using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using moviesStorage.IdentityService.Data.Identity;

namespace moviesStorage.IdentityService.ServiceContext;

public class IdentityContext : IdentityDbContext<ServiceUser>
{
    public IdentityContext(DbContextOptions options) : base(options)
    {
        
    }

    public void SeedTestUser()
    {
        Users.Add(new ServiceUser()
        {
            UserTitle = "Mr.",
            Firstname = "Katharina",
            MiddleName = "ARCHIVE",
            LastName = "Spencer",
            PlaceOfBirth = "Creminside",
            DateOfBirth = DateTime.Today,
            Gender = "1675358192",
            Line1 = "A",
            Line2 = "B",
            Town = "Test",
            Country = "Finland",
            PostCode = "TWD",
            CountryCode = "GN",
            UserName = "testUser",
            NormalizedUserName = "TESTUSER",
            Email = "Mazie.Spinka2@gmail.com",
            NormalizedEmail = "MAZIE.SPINKA2@GMAIL.COM",
            EmailConfirmed = false,
            PasswordHash = "AQAAAAIAAYagAAAAEIH036nETniTrXx6nDX4OXbICgTEXdZPd3dGqDFC8n8iLtlL40QoqySsxiZu0LQTWQ==",
            SecurityStamp = "MLBXMRPJK6ZO6NZR6ZAFQT5VUOFTN3VR",
            PhoneNumber = "628-639-1465",
            PhoneNumberConfirmed = false,
            LockoutEnabled = true,
            AccessFailedCount = 0
        });
        SaveChanges();
    }
}