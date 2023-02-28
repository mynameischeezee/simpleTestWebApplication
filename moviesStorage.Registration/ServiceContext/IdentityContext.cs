using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using moviesStorage.Registration.Data.Identity;

namespace moviesStorage.Registration.ServiceContext;

public class IdentityContext : IdentityDbContext<ServiceUser>
{
    public IdentityContext(DbContextOptions options) : base(options)
    {
        
    }
}