using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using movieStorage.Registration.Data.Identity;

namespace movieStorage.Registration.ServiceContext;

public class IdentityContext : IdentityDbContext<ServiceUser>
{
    public IdentityContext(DbContextOptions options) : base(options)
    {
        
    }
}