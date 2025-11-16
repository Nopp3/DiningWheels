using DiningWheels.Application.Restaurants.Queries;
using DiningWheels.Domain.Entities;
using DiningWheels.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DiningWheels.Tests.Restaurants;

public class RestaurantQueryFiltersTests
{
    private DiningWheelsDbContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<DiningWheelsDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new DiningWheelsDbContext(options);
    }

    [Fact]
    public async Task Handle_FiltersOutInactiveRestaurants()
    {
        var context = GetInMemoryContext();
        context.Restaurants.AddRange(
            new Restaurant {Name = "Active", Location = new Location {Address = "A", Latitude = 1, Longitude = 1 }},
            new Restaurant {Name = "Inactive", Location = new Location {Address = "B", Latitude = 1, Longitude = 1 }, IsActive = false}
        );
        await context.SaveChangesAsync();
        
        var handler = new GetRestaurantsQueryHandler(context);
        var result = await handler.Handle(new GetRestaurantsQuery(), CancellationToken.None);
        
        Assert.Single(result);
        Assert.Equal("Active", result.First().Name);
    }
    
    [Fact]
    public async Task IgnoreQueryFilters_ReturnsAllRestaurants()
    {
        var context = GetInMemoryContext();
        context.Restaurants.AddRange(
            new Restaurant {Name = "Active", Location = new Location {Address = "A", Latitude = 1, Longitude = 1 }},
            new Restaurant {Name = "Inactive", Location = new Location {Address = "B", Latitude = 1, Longitude = 1 }, IsActive = false}
        );
        await context.SaveChangesAsync();
        
        var restaurants = await context.Restaurants.IgnoreQueryFilters().ToListAsync();
        
        Assert.Equal(2, restaurants.Count);
    }
}