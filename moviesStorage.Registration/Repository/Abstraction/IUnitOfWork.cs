using moviesStorage.Registration.Data.Identity;

namespace moviesStorage.Registration.Repository.Abstraction;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<ServiceUser> Users{get;}
    
    Task Save();
}