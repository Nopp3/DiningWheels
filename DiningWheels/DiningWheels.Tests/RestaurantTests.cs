using DiningWheels.Application.Restaurants.Commands;
using DiningWheels.Application.Restaurants.Queries;
using DiningWheels.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DiningWheels.Tests;

public class RestaurantTests
{
    private readonly CreateRestaurantCommandValidator _validator = new();
    
    private DiningWheelsDbContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<DiningWheelsDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new DiningWheelsDbContext(options);
    }

    [Fact]
    public async Task CreateRestaurant_And_GetRestaurants_Works()
    {
        var context = GetInMemoryContext();
        var createHandler = new CreateRestaurantCommandHandler(context);
        var queryHandler = new GetRestaurantsQueryHandler(context);
        
        var command = new CreateRestaurantCommand("Test Restaurant", "Test Address", 10.0, 20.0, "password");
        
        var id = await createHandler.Handle(command, CancellationToken.None);
        var restaurants = await queryHandler.Handle(new GetRestaurantsQuery(), CancellationToken.None);
        
        Assert.NotEmpty(restaurants);
        Assert.Equal("Test Restaurant", restaurants.First().Name);
        Assert.Equal(id, restaurants.First().Id);
    }
    
    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var command = new CreateRestaurantCommand("", "Test Address", 10.0, 20.0, "password");
        var result = _validator.Validate(command);
        
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name");
    }

    [Fact]
    public void Should_Have_Error_When_Password_Too_Short()
    {
        var command = new CreateRestaurantCommand("Name", "Test Address", 10.0, 20.0, "123");
        var result = _validator.Validate(command);
        
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("Password must be at least 6 characters"));
    }

    [Fact]
    public void Should_Pass_When_Valid_Command()
    {
        var command = new CreateRestaurantCommand("Name", "Address", 10.0, 20.0, "123456");
        var result = _validator.Validate(command);
        
        Assert.True(result.IsValid);
    }
}