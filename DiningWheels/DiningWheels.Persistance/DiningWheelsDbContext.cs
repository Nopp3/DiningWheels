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
        modelBuilder.Entity<User>().HasQueryFilter(u => u.IsActive);
        modelBuilder.Entity<Restaurant>().HasQueryFilter(r => r.IsActive);
        modelBuilder.Entity<MenuItem>().HasQueryFilter(m => m.IsActive);        
        modelBuilder.Entity<Order>().HasQueryFilter(o => o.IsActive);
        modelBuilder.Entity<ContactMessage>().HasQueryFilter(c => c.IsActive);
        modelBuilder.Entity<OpeningHours>().HasQueryFilter(o => o.IsActive);
        modelBuilder.Entity<OrderItem>().HasQueryFilter(oi => oi.IsActive);    
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
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