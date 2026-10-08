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

    // LOWER() в SQLite работает только с латиницей, поэтому
    // сравнение без учёта регистра делается уже в памяти
    public IEnumerable<Client> SearchByName(string part) =>
        Set.AsEnumerable()
           .Where(c => c.FullName.Contains(part, StringComparison.CurrentCultureIgnoreCase))
           .OrderBy(c => c.FullName)
           .ToList();

    public Client? GetWithCars(int id) =>
        Set.Include(c => c.Cars).FirstOrDefault(c => c.Id == id);
}
