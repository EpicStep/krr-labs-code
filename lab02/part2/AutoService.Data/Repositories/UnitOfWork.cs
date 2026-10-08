namespace AutoService.Data.Repositories;

public interface IUnitOfWork : IDisposable
{
    IClientRepository Clients { get; }
    ICarRepository Cars { get; }
    IServiceRepository Services { get; }

    int SaveChanges();
    Task<int> SaveChangesAsync();
}

// Все репозитории работают с одним контекстом, поэтому изменения
// нескольких репозиториев сохраняются одной транзакцией
public class UnitOfWork : IUnitOfWork
{
    private readonly AutoServiceContext _context;

    public IClientRepository Clients { get; }
    public ICarRepository Cars { get; }
    public IServiceRepository Services { get; }

    public UnitOfWork(AutoServiceContext context)
    {
        _context = context;
        Clients = new ClientRepository(context);
        Cars = new CarRepository(context);
        Services = new ServiceRepository(context);
    }

    public int SaveChanges() => _context.SaveChanges();

    public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
