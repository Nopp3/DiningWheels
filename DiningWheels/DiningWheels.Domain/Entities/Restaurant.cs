using DiningWheels.Domain.Common;

namespace DiningWheels.Domain.Entities;

public class Restaurant : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; } = "";
    public Location Location { get; set; }
    public string PasswordHash { get; set; }
    
    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    
    public virtual ICollection<OpeningHours> OpeningHours { get; set; } = new List<OpeningHours>();
    
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
