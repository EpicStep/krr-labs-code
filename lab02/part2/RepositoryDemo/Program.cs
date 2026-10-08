using AutoService.Data;
using AutoService.Data.Entities;
using AutoService.Data.Repositories;
using Microsoft.EntityFrameworkCore;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var dbPath = Path.Combine(AppContext.BaseDirectory, "autoservice.db");
var options = new DbContextOptionsBuilder<AutoServiceContext>()
    .UseSqlite($"Data Source={dbPath}")
    .Options;

// База пересоздаётся при каждом запуске, чтобы вывод был одинаковым
using (var ctx = new AutoServiceContext(options))
{
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
}

using var uow = new UnitOfWork(new AutoServiceContext(options));

Console.WriteLine("--- Create ---");
var ivanov = new Client { FullName = "Иванов Пётр Сергеевич", Phone = "+79161234567", Email = "ivanov@mail.ru" };
var smirnova = new Client { FullName = "Смирнова Анна Олеговна", Phone = "+79035554433" };
uow.Clients.Add(ivanov);
uow.Clients.Add(smirnova);
uow.Cars.Add(new Car { Brand = "Lada", Model = "Vesta", Year = 2019, LicensePlate = "А123ВС77", Client = ivanov });
uow.Cars.Add(new Car { Brand = "Kia", Model = "Rio", Year = 2015, LicensePlate = "О777ОО99", Client = ivanov });
uow.Cars.Add(new Car { Brand = "Toyota", Model = "Camry", Year = 2021, LicensePlate = "М001ММ50", Client = smirnova });
uow.Services.Add(new Service { Name = "Замена масла", Price = 1200m, DurationMinutes = 30 });
uow.Services.Add(new Service { Name = "Диагностика подвески", Price = 900m, DurationMinutes = 40 });
uow.Services.Add(new Service { Name = "Замена тормозных колодок", Price = 2500m, DurationMinutes = 60 });
Console.WriteLine($"Сохранено записей: {uow.SaveChanges()}");

Console.WriteLine("--- Read ---");
foreach (var c in uow.Clients.GetAll())
    Console.WriteLine($"{c.Id}: {c.FullName}, {c.Phone}");
var withCars = uow.Clients.GetWithCars(ivanov.Id)!;
Console.WriteLine($"Автомобили клиента {withCars.FullName}: " +
                  string.Join(", ", withCars.Cars.Select(c => $"{c.Brand} {c.Model} ({c.LicensePlate})")));

Console.WriteLine("--- Специальные методы ---");
Console.WriteLine($"Поиск по телефону +79035554433: {uow.Clients.FindByPhone("+79035554433")?.FullName}");
Console.WriteLine($"Поиск по имени «анна»: {string.Join("; ", uow.Clients.SearchByName("анна").Select(c => c.FullName))}");
Console.WriteLine($"Авто по номеру М001ММ50: {uow.Cars.FindByPlate("М001ММ50")?.Model}");
Console.WriteLine("Авто 2016–2022 годов: " +
                  string.Join(", ", uow.Cars.GetByYearRange(2016, 2022).Select(c => $"{c.Model} {c.Year}")));
Console.WriteLine("Услуги от 1000 до 3000 руб.: " +
                  string.Join(", ", uow.Services.GetByPriceRange(1000m, 3000m).Select(s => $"{s.Name} ({s.Price} руб.)")));

Console.WriteLine("--- Update ---");
var service = uow.Services.GetById(1)!;
service.Price = 1350m;
uow.Services.Update(service);
uow.SaveChanges();
Console.WriteLine($"Новая цена «{service.Name}»: {uow.Services.GetById(1)!.Price} руб.");

Console.WriteLine("--- Delete ---");
uow.Clients.Delete(smirnova.Id);
uow.SaveChanges();
Console.WriteLine($"Клиентов: {uow.Clients.GetAll().Count()}, автомобилей: {uow.Cars.GetAll().Count()}");

Console.WriteLine("--- Async ---");
await uow.Services.AddAsync(new Service { Name = "Развал-схождение", Price = 1800m, DurationMinutes = 45 });
await uow.SaveChangesAsync();
var all = await uow.Services.GetAllAsync();
Console.WriteLine($"Услуг в базе: {all.Count}, последняя: {(await uow.Services.GetByIdAsync(all.Max(s => s.Id)))!.Name}");
