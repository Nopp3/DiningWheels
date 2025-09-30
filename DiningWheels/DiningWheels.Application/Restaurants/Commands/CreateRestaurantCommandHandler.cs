namespace DiningWheels.Application.Restaurants.Commands;
using Common.Interfaces;
using Domain.Entities;
using MediatR;

public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, Guid>
{
    private readonly IDiningWheelsDbContext _context;

    public CreateRestaurantCommandHandler(IDiningWheelsDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = new Restaurant
        {
            Name = request.Name,
            Location = new Location
            {
                Address = request.Address,
                Latitude = request.Latitude,
                Longitude = request.Longitude
            },
            PasswordHash = request.Password // Hashed in future
        };

        _context.Restaurants.Add(restaurant);
        await _context.SaveChangesAsync(cancellationToken);

        return restaurant.Id;
    }
}