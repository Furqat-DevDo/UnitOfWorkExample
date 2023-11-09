using UnitOfWorkExample.Repositories;

namespace UnitOfWorkExample.UnitOfWork;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IAddressRepository Addresses { get; }
    Task StartTransaction();
    Task RollBackTransaction();
    Task CommitTransaction();
    Task SaveChanges();
}