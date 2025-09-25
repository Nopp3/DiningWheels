using System.Reflection;
using DiningWheels.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using DiningWheels.Domain.Common;
using DiningWheels.Domain.Entities;

namespace DiningWheels.Persistance;

public class DiningWheelsDbContext : DbContext, IDiningWheelsDbContext
{
    public DiningWheelsDbContext(DbContextOptions<DiningWheelsDbContext> options) : base(options)
    {
    }
    
    public DbSet<ContactMessage> ContactMessages { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<OpeningHours> OpeningHours { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Id = Guid.NewGuid();
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Deleted:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.IsActive = false;
                    entry.State = EntityState.Modified;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}