using DiningWheels.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DiningWheels.Application.Restaurants.Commands;
using Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.Infrastructure;

    
public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, Guid>
{
    private readonly IDiningWheelsDbContext _context;

    public CreateRestaurantCommandHandler(IDiningWheelsDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var owner = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.OwnerEmail, cancellationToken);

        if (owner is null)
        {
            throw new InvalidOperationException($"User with email {request.OwnerEmail} not found.");    
        }
        
        var restaurant = new Restaurant
        {
            Name = request.Name,
            Location = new Location
            {
                Address = request.Address,
                Latitude = request.Latitude,
                Longitude = request.Longitude
            },
            Owner = owner
        };
        
        owner.Role = Role.Owner;
        owner.Restaurants.Add(restaurant);
        
        if (_context.Database.IsRelational())
        {
            await using var tx = await _context.Database.BeginTransactionAsync(cancellationToken);

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync(cancellationToken);
            await tx.CommitAsync(cancellationToken);
        }
        else
        {
            // For tests purposes (InMemory)
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        return restaurant.Id;
    }
}