using movieStorage.Registration.Data.Identity;

namespace movieStorage.Registration.Repository.Abstraction;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<ServiceUser> Users{get;}
    
    Task Save();
}