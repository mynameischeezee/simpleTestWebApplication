using moviesStorage.IdentityService.Data.Identity;

namespace moviesStorage.IdentityService.Repository.Abstraction;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<ServiceUser> Users{get;}
    
    Task Save();
}