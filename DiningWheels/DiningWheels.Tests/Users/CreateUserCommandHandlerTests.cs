using DiningWheels.Persistance;
using DiningWheels.Application.Users.Commands;
using DiningWheels.Domain.Entities;
using DiningWheels.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DiningWheels.Tests.Users;

public class CreateUserCommandHandlerTests
{
    private DiningWheelsDbContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<DiningWheelsDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new DiningWheelsDbContext(options);
    }

    [Fact]
    public async Task Handle_CreateUserCommandHandler_WorksCorrectly()
    {
        var context = GetInMemoryContext();
        var handler = new CreateUserCommandHandler(context);

        var command = new CreateUserCommand("user@example.com", "John Doe", "Abcd1234");
        var id = await handler.Handle(command, CancellationToken.None);
        
        var user = await context.Users.FindAsync(id);
        
        Assert.NotNull(user);
        Assert.Equal(Role.Customer, user.Role);
        Assert.Equal("John", user.FirstName);
        Assert.Equal("Doe", user.LastName);
    }
    
    [Fact]
    public async Task Handle_UserAlreadyExists_ThrowsInvalidOperation()
    {
        var context = GetInMemoryContext();
        var handler = new CreateUserCommandHandler(context);
        
        context.Users.Add(new User { Email = "user@example.com", FirstName = "John", LastName = "Doe", PasswordHash = "Abcd1234" });
        await context.SaveChangesAsync();
        
        var command = new CreateUserCommand("user@example.com", "John Doe", "Abcd1234");
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));
    }
}