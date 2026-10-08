using Microsoft.EntityFrameworkCore;

namespace UsersApi.Data;

public class UsersContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public UsersContext(DbContextOptions<UsersContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("User");
        modelBuilder.Entity<User>().Property(u => u.Login).HasMaxLength(50).IsRequired();
        modelBuilder.Entity<User>().Property(u => u.PassHash).HasMaxLength(64).IsRequired();
        modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();
    }
}
