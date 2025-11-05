using DiningWheels.Application.Restaurants.Commands;

namespace DiningWheels.Tests.Restaurants;

public class CreateRestaurantCommandValidatorTests
{
    private readonly CreateRestaurantCommandValidator _validator = new();

    [Theory]
    [InlineData("", "Address", 10, 20, "owner@example.com", "Name")]
    [InlineData("Name", "", 10, 20, "owner@example.com", "Address")]
    [InlineData("Name", "Address", 10, 20, "", "OwnerEmail")]
    [InlineData("Name", "Address", 10, 20, "ownerexamplecom", "OwnerEmail")]
    public void Validate_InvalidInputs_ReturnsErrors(string name, string address, double latitude, double longitude,
        string email, string field)
    {
        var command = new CreateRestaurantCommand(name, address, latitude, longitude, email);
        var result = _validator.Validate(command);
        
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == field);
    }

    [Fact]
    public void Validate_ValidCommand_Passes()
    {
        var command = new CreateRestaurantCommand("Name", "Address", 10, 20, "owner@example.com");
        var result = _validator.Validate(command);
        
        Assert.True(result.IsValid);       
    }
}