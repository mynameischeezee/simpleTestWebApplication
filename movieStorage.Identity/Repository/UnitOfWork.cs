using movieStorage.Identity.Data.Identity;
using movieStorage.Identity.Repository.Abstraction;
using movieStorage.Identity.ServiceContext;

namespace movieStorage.Identity.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly IdentityContext _context;
    private IGenericRepository<ServiceUser> _users;

    public IGenericRepository<ServiceUser> Users => _users ??= new GenericRepository<ServiceUser>(_context);

    public UnitOfWork(IdentityContext context)
    {
        _context = context;
    }
    
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
    
    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}