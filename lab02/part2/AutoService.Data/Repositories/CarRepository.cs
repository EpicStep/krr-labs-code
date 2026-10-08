using AutoService.Data.Entities;

namespace AutoService.Data.Repositories;

public class CarRepository : GenericRepository<Car>, ICarRepository
{
    public CarRepository(AutoServiceContext context) : base(context)
    {
    }

    public IEnumerable<Car> GetByClient(int clientId) =>
        Set.Where(c => c.ClientId == clientId).ToList();

    public Car? FindByPlate(string licensePlate) =>
        Set.FirstOrDefault(c => c.LicensePlate == licensePlate);

    public IEnumerable<Car> GetByYearRange(int from, int to) =>
        Set.Where(c => c.Year >= from && c.Year <= to).OrderBy(c => c.Year).ToList();
}
