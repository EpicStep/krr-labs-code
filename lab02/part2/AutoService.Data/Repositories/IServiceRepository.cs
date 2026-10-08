using AutoService.Data.Entities;

namespace AutoService.Data.Repositories;

public interface IServiceRepository : IRepository<Service>
{
    IEnumerable<Service> GetByPriceRange(decimal min, decimal max);
}

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    public ServiceRepository(AutoServiceContext context) : base(context)
    {
    }

    public IEnumerable<Service> GetByPriceRange(decimal min, decimal max) =>
        Set.Where(s => s.Price >= min && s.Price <= max).OrderBy(s => s.Price).ToList();
}
