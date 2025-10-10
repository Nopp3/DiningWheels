using DiningWheels.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace DiningWheels.Application.Common.Interfaces;

public interface IDiningWheelsDbContext
{
    public DbSet<ContactMessage> ContactMessages { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<OpeningHours> OpeningHours { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<User> Users { get; set; }
    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}