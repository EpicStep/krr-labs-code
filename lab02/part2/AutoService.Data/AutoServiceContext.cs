using AutoService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Data;

public class AutoServiceContext : DbContext
{
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<Service> Services => Set<Service>();

    public AutoServiceContext(DbContextOptions<AutoServiceContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().Property(c => c.FullName).HasMaxLength(150).IsRequired();
        modelBuilder.Entity<Client>().HasIndex(c => c.Phone).IsUnique();

        modelBuilder.Entity<Car>().HasIndex(c => c.LicensePlate).IsUnique();
        modelBuilder.Entity<Car>()
            .HasOne(c => c.Client)
            .WithMany(c => c.Cars)
            .HasForeignKey(c => c.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        // SQLite не умеет сравнивать decimal, поэтому цена хранится как double
        modelBuilder.Entity<Service>().Property(s => s.Price).HasConversion<double>();
    }
}
