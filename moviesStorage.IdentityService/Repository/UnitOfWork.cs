using moviesStorage.IdentityService.Data.Identity;
using moviesStorage.IdentityService.Repository.Abstraction;
using moviesStorage.IdentityService.ServiceContext;

namespace moviesStorage.IdentityService.Repository;

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