using DiningWheels.Application.Restaurants.Queries;
using DiningWheels.Domain.Entities;
using DiningWheels.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DiningWheels.Tests.Restaurants;

public class GetRestaurantsQueryHandlerTests
{
    private DiningWheelsDbContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<DiningWheelsDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new DiningWheelsDbContext(options);
    }

    [Fact]
    public async Task Handle_GetRestaurantsQueryHandler_WorksCorrectly()
    {
        var context = GetInMemoryContext();
        context.Restaurants.AddRange(
            new Restaurant {Name = "Restaurant 1", Location = new Location {Address = "Address 1", Latitude = 10, Longitude = 20}},
            new Restaurant {Name = "Restaurant 2", Location = new Location {Address = "Address 2", Latitude = 10, Longitude = 20}}
            );
        await context.SaveChangesAsync();

        var handler = new GetRestaurantsQueryHandler(context);
        var result = await handler.Handle(new GetRestaurantsQuery(), CancellationToken.None);
        
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count);
    }
    
    [Fact]
    public async Task Handle_Returns_OnlyActiveRestaurants()
    {
        var context = GetInMemoryContext();
        context.Restaurants.AddRange(
            new Restaurant {Name = "Restaurant 1", Location = new Location {Address = "Address 1", Latitude = 10, Longitude = 20}},
            new Restaurant {Name = "Restaurant 2", Location = new Location {Address = "Address 2", Latitude = 10, Longitude = 20}, IsActive = false}
        );
        await context.SaveChangesAsync();

        var handler = new GetRestaurantsQueryHandler(context);
        var result = await handler.Handle(new GetRestaurantsQuery(), CancellationToken.None);
        
        Assert.Single(result);
        Assert.Equal("Restaurant 1", result.First().Name);
    }
}