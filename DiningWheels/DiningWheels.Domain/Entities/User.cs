using System.Collections.Generic;
using DiningWheels.Domain.Common;

namespace DiningWheels.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    public string Email     { get; set; }
    public string Phone     { get; set; }
    public Location Location { get; set; }
    public string PasswordHash { get; set; }
    
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
