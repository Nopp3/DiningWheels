using DiningWheels.Domain.Entities;

namespace DiningWheels.Application.Restaurants.Queries;

public class RestaurantDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
}