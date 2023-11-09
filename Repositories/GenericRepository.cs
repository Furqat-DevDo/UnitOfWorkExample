using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;


namespace UnitOfWorkExample.Repositories;

public class GenericRepository<TEntity,TDbContext> : IGenericRepository<TEntity> 
    where TEntity : class
    where TDbContext : DbContext
{
    protected TDbContext context;
    protected DbSet<TEntity> dbSet;
    public GenericRepository(
        TDbContext context)
    {
        this.context = context;
        dbSet = context.Set<TEntity>();
    }
    
    public virtual async Task<IEnumerable<TEntity>> All()
    {
       return await  dbSet.ToListAsync();
    }

    public virtual async Task<TEntity?> GetById(Guid id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual async Task<bool> Add(TEntity entity)
    {
        await dbSet.AddAsync(entity);
        return true;
    }

    public virtual async Task<bool> Delete(Guid id)
    {
       var entity = await dbSet.FindAsync(id);
       if (entity is null) return false;

       dbSet.Remove(entity);
       return true;
    }

    public virtual Task<bool> Update(TEntity entity)
    {
        context.Update(entity);
        return Task.FromResult(true);
    }

    public virtual async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return await dbSet.Where(predicate).ToListAsync();
    }
}