using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using moviesStorage.IdentityService.Data.Identity;

namespace moviesStorage.IdentityService.ServiceContext;

public class IdentityContext : IdentityDbContext<ServiceUser>
{
    public IdentityContext(DbContextOptions options) : base(options)
    {
        
    }
}