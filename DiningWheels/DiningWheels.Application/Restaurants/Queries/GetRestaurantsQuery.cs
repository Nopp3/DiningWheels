using MediatR;

namespace DiningWheels.Application.Restaurants.Queries;

public record GetRestaurantsQuery : IRequest<List<RestaurantDto>>;