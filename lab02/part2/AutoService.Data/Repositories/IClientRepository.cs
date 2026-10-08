using AutoService.Data.Entities;

namespace AutoService.Data.Repositories;

public interface IClientRepository : IRepository<Client>
{
    Client? FindByPhone(string phone);
    IEnumerable<Client> SearchByName(string part);
    Client? GetWithCars(int id);
}
