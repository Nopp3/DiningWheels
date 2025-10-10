using DiningWheels.Application.Restaurants.Commands;
using DiningWheels.Application.Restaurants.Queries;
using DiningWheels.Domain.Entities;
using DiningWheels.Domain.Enums;
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
    public async Task CreateRestaurant_And_GetRestaurants_AndChangeUserRole_Works()
    {
        var context = GetInMemoryContext();

        var owner = new User
        {
            Email = "owner@example.com",
            FirstName = "Owner",
            LastName = "Test",
            PasswordHash = "Abcd1234"
        };
        context.Users.Add(owner);
        await context.SaveChangesAsync();
        
        var createHandler = new CreateRestaurantCommandHandler(context);
        var queryHandler = new GetRestaurantsQueryHandler(context);
        
        var command = new CreateRestaurantCommand("Test Restaurant", "Test Address", 10.0, 20.0, "owner@example.com");
        
        var id = await createHandler.Handle(command, CancellationToken.None);
        
        var updatedOwner = await context.Users
            .Include(u => u.Restaurants)
            .FirstAsync(u => u.Email == "owner@example.com");
        var restaurants = await queryHandler.Handle(new GetRestaurantsQuery(), CancellationToken.None);
        
        Assert.NotEmpty(restaurants);
        Assert.Equal("Test Restaurant", restaurants.First().Name);
        Assert.Equal(id, restaurants.First().Id);
        Assert.Equal(Role.Owner, updatedOwner.Role);
        Assert.Contains(updatedOwner.Restaurants, r => r.Id == id);
    }
    
    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var command = new CreateRestaurantCommand("", "Test Address", 10.0, 20.0, "owner@example.com");
        var result = _validator.Validate(command);
        
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name");
    }
    
    [Fact]
    public void Should_Have_Error_When_Address_Is_Empty()
    {
        var command = new CreateRestaurantCommand("test", "", 10.0, 20.0, "owner@example.com");
        var result = _validator.Validate(command);
        
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Address");
    }

    [Fact]
    public void Should_Have_Error_When_Invalid_Email()
    {
        var command = new CreateRestaurantCommand("Name", "Test Address", 10.0, 20.0, "123");
        var result = _validator.Validate(command);
        
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("Invalid Email Address"));
    }
}