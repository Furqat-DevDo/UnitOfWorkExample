using UnitOfWorkExample.Data;
using UnitOfWorkExample.Repositories;

namespace UnitOfWorkExample.UnitOfWork;

public class UnitOfWork : IUnitOfWork,IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;
    public IUserRepository Users { get; }
    public IAddressRepository Addresses { get; }

    public UnitOfWork(ApplicationDbContext context,ILogger<UnitOfWork> logger)
    {
        _context = context;
        _logger = logger;
        
        Users = new UserRepository(context,logger);
        Addresses = new AddressRepository(context);
    }
    
    public async Task StartTransaction()
    {
        _logger.LogInformation($"Time: {DateTime.UtcNow} Transaction started.");
        await _context.Database.BeginTransactionAsync();
    }

    public async Task RollBackTransaction()
    {
        _logger.LogInformation($"Time: {DateTime.UtcNow} Transaction rolled back.");
        await _context.Database.RollbackTransactionAsync();
    }

    public async Task CommitTransaction()
    {
        _logger.LogInformation($"Time: {DateTime.UtcNow} Transaction committed.");
        await _context.SaveChangesAsync();
        await _context.Database.CommitTransactionAsync();
    }
    
    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}