using Microsoft.EntityFrameworkCore;

namespace AutoService.Data.Repositories;

// Универсальный репозиторий для любой сущности контекста.
// SaveChanges здесь не вызывается – изменения сохраняет UnitOfWork.
public class GenericRepository<T> : IRepository<T> where T : class
{
    protected readonly AutoServiceContext Context;
    protected readonly DbSet<T> Set;

    public GenericRepository(AutoServiceContext context)
    {
        Context = context;
        Set = context.Set<T>();
    }

    public T? GetById(int id) => Set.Find(id);

    public IEnumerable<T> GetAll() => Set.AsNoTracking().ToList();

    public void Add(T entity) => Set.Add(entity);

    public void Update(T entity) => Set.Update(entity);

    public void Delete(int id)
    {
        var entity = Set.Find(id);
        if (entity != null)
            Set.Remove(entity);
    }

    public async Task<T?> GetByIdAsync(int id) => await Set.FindAsync(id);

    public Task<List<T>> GetAllAsync() => Set.AsNoTracking().ToListAsync();

    public async Task AddAsync(T entity) => await Set.AddAsync(entity);
}
