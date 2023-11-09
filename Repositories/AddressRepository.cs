using UnitOfWorkExample.Data;
using UnitOfWorkExample.Entities;

namespace UnitOfWorkExample.Repositories;

public class AddressRepository : GenericRepository<Address,ApplicationDbContext>,IAddressRepository
{
    public AddressRepository(ApplicationDbContext context) : base(context)
    {
    }
    
}