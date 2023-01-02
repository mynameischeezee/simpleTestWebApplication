using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using movieStorage.Identity.Data.Identity;

namespace movieStorage.Identity.ServiceContext;

public class IdentityContext : IdentityDbContext<ServiceUser>
{
    public IdentityContext(DbContextOptions options) : base(options)
    {
        
    }
}