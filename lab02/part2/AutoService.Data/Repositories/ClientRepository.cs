using AutoService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Data.Repositories;

public class ClientRepository : GenericRepository<Client>, IClientRepository
{
    public ClientRepository(AutoServiceContext context) : base(context)
    {
    }

    public Client? FindByPhone(string phone) =>
        Set.FirstOrDefault(c => c.Phone == phone);

    public IEnumerable<Client> SearchByName(string part) =>
        Set.Where(c => c.FullName.ToLower().Contains(part.ToLower()))
           .OrderBy(c => c.FullName)
           .ToList();

    public Client? GetWithCars(int id) =>
        Set.Include(c => c.Cars).FirstOrDefault(c => c.Id == id);
}
