using DiningWheels.Application.Common.Interfaces;
using DiningWheels.Application.Restaurants.Commands;
using DiningWheels.Application.Restaurants.Queries;
using DiningWheels.Domain.Entities;
using DiningWheels.Domain.Enums;
using DiningWheels.Persistance;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DiningWheels.Tests.Restaurants;

public class FullRestaurantCreationFlowTests
{
    private DiningWheelsDbContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<DiningWheelsDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new DiningWheelsDbContext(options);
    }

    [Fact]
    public async Task Create_FullFlow_WorksCorrectly()
    {
        var context = GetInMemoryContext();
        
        var owner = new User {Email = "owner@example.com", FirstName = "John", LastName = "Doe", PasswordHash = "password"};
        context.Users.Add(owner);
        await context.SaveChangesAsync();

        var email = new Mock<IEmailService>();
        var createHandler = new CreateRestaurantCommandHandler(context, email.Object);
        var queryHandler = new GetRestaurantsQueryHandler(context);
        
        Assert.Equal(Role.Customer, owner.Role);
        
        var command = new CreateRestaurantCommand("Name", "Address", 10, 20, owner.Email);
        
        var id = await createHandler.Handle(command, CancellationToken.None);
        var restaurant = await queryHandler.Handle(new GetRestaurantsQuery(), CancellationToken.None);
        var updatedOwner = await context.Users.Include(u => u.Restaurants).FirstAsync();
        
        Assert.Single(restaurant);
        Assert.Equal(Role.Owner, updatedOwner.Role);
        Assert.Equal(id, updatedOwner.Restaurants.First().Id);
        
        email.Verify(e => e.SendEmailAsync(owner.Email, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}