using movieStorage.Identity.Data.Identity;

namespace movieStorage.Identity.Repository.Abstraction;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<ServiceUser> Users{get;}
    
    Task Save();
}