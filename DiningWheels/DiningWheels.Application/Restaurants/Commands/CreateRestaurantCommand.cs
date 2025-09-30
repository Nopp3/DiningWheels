namespace DiningWheels.Application.Restaurants.Commands;

using MediatR;

public record CreateRestaurantCommand(
    string Name,
    string Address,
    double Latitude,
    double Longitude,
    string Password
) : IRequest<Guid>;
