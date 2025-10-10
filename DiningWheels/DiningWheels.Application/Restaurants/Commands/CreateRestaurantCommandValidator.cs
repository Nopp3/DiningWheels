namespace DiningWheels.Application.Restaurants.Commands;

using FluentValidation;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    public CreateRestaurantCommandValidator()
    {
        RuleFor(r => r.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(r => r.Address).NotEmpty().WithMessage("Address is required");
        RuleFor(r => r.OwnerEmail).NotEmpty().WithMessage("Owner's Email is required")
            .EmailAddress().WithMessage("Invalid Email Address");
    }
}