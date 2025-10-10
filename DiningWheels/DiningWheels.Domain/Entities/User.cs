using System.Collections.Generic;
using DiningWheels.Domain.Common;
using DiningWheels.Domain.Enums;

namespace DiningWheels.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public Location? Location { get; set; }
    public string PasswordHash { get; set; }
    public Role Role { get; set; } = Role.Customer;
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}
