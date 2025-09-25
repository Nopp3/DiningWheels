using DiningWheels.Domain.Common;

namespace DiningWheels.Domain.Entities;

using System;

public class MenuItem : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    
    // If null the dish is assigned to every restaurant
    public Guid? RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }
}
