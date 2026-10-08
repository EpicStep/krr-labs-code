using AutoService.Data.Entities;

namespace AutoService.Data.Repositories;

public interface ICarRepository : IRepository<Car>
{
    IEnumerable<Car> GetByClient(int clientId);
    Car? FindByPlate(string licensePlate);
    IEnumerable<Car> GetByYearRange(int from, int to);
}
