using Microsoft.EntityFrameworkCore;
using UnitOfWorkExample.Data;
using UnitOfWorkExample.Entities;

namespace UnitOfWorkExample.Repositories;

public class UserRepository : GenericRepository<User,ApplicationDbContext>, IUserRepository
{
    private readonly ILogger _logger;
    public UserRepository(ApplicationDbContext context, 
        ILogger logger) 
        : base(context)
    {
        _logger = logger;
    }
    
    public override async Task<bool> Update(User entity)
    {
        try
        {
            var existingUser = await dbSet.Where(x => x.Id == entity.Id)
                .FirstOrDefaultAsync();

            if (existingUser == null)
                return await Add(entity);

            existingUser.FirstName = entity.FirstName;
            existingUser.LastName = entity.LastName;
            existingUser.Email = entity.Email;

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Update function error", typeof(UserRepository));
            return false;
        }
    }
    
    public override async Task<bool> Delete(Guid id)
    {
        try
        {
            var exist = await dbSet.Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (exist == null) return false;

            dbSet.Remove(exist);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Delete function error", typeof(UserRepository));
            return false;
        }
    }
}