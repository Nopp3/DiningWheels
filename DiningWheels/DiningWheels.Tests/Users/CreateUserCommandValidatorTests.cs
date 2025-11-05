using DiningWheels.Application.Users.Commands;

namespace DiningWheels.Tests.Users;

public class CreateUserCommandValidatorTests
{
    private readonly CreateUserCommandValidator _validator = new();
    
    [Theory]
    [InlineData("", "Abcd1234", "owner@example.com", "FullName")]
    [InlineData("Full Name", "short", "owner@example.com", "Password")]
    [InlineData("Full Name", "", "owner@example.com", "Password")]
    [InlineData("Full Name", "Abcd1234", "", "Email")]
    [InlineData("Full Name", "Abcd1234", "ownerexamplecom", "Email")]
    public void Validate_CreateUserCommand_FailsValidation(string fullName, string password, string email, string field)
    {
        var command = new CreateUserCommand(email, fullName, password);
        var result = _validator.Validate(command);
        
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == field);
    }

    [Fact]
    public void Validate_ValidCommand_Passes()
    {
        var command = new CreateUserCommand("owner@example.com", "Full Name", "Abcd1234");
        var result = _validator.Validate(command);
        
        Assert.True(result.IsValid);
    }
}