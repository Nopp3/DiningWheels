using DiningWheels.Application.Common.Interfaces;
using DiningWheels.Application.Restaurants.Commands;
using DiningWheels.Domain.Entities;
using DiningWheels.Domain.Enums;
using DiningWheels.Persistance;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DiningWheels.Tests.Restaurants;

public class CreateRestaurantCommandHandlerTests
{
    private DiningWheelsDbContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<DiningWheelsDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new DiningWheelsDbContext(options);
    }

    [Fact]
    public async Task Handle_CreateRestaurantCommandHandler_WorksCorrectly()
    {
        var context = GetInMemoryContext();
        var owner = new User
        {
            FirstName = "Owner",
            Email = "owner@example.com",
            PasswordHash = "password"
        };
        context.Users.Add(owner);
        await context.SaveChangesAsync();

        var email = new Mock<IEmailService>();
        var handler = new CreateRestaurantCommandHandler(context, email.Object);
        
        var command = new CreateRestaurantCommand("Name", "Address", 10, 20, owner.Email);
        var id = await handler.Handle(command, CancellationToken.None);
        
        var restaurant = await context.Restaurants.Include(r => r.Owner).FirstAsync();
        
        Assert.Equal("Name", restaurant.Name);
        Assert.Equal(owner.Id, restaurant.OwnerId);
        Assert.Equal(Role.Owner, owner.Role);
    }

    [Fact]
    public async Task Handle_UserNotFound_ThrowsInvalidOperation()
    {
        var context = GetInMemoryContext();
        var email = new Mock<IEmailService>();
        var handler = new CreateRestaurantCommandHandler(context, email.Object);

        var command = new CreateRestaurantCommand("N", "A", 10, 20, "none@example.com");
        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));       
    }
}