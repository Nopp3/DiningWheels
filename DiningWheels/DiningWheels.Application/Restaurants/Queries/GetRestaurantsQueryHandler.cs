using DiningWheels.Application.Common.Interfaces;

namespace DiningWheels.Application.Restaurants.Queries;

using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetRestaurantsQueryHandler : IRequestHandler<GetRestaurantsQuery, List<RestaurantDto>>
{
    private readonly IDiningWheelsDbContext _context;
    
    public GetRestaurantsQueryHandler(IDiningWheelsDbContext context)
    {
        _context = context;
    }

    public async Task<List<RestaurantDto>> Handle(GetRestaurantsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Restaurants.Where(x => x.IsActive)
            .Select(x => new RestaurantDto
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Location.Address
            }).ToListAsync(cancellationToken);
    }
}