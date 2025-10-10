using DiningWheels.Domain.Common;
using DiningWheels.Domain.Enums;

namespace DiningWheels.Domain.Entities;

public class Order : BaseEntity
{
    public Guid RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }
    
    public Guid? UserId { get; set; }
    public virtual User? User { get; set; }
    
    public string CustomerFirstName { get; set; }
    public string CustomerLastName  { get; set; }
    public string CustomerPhone     { get; set; }
    public Location DeliveryLocation { get; set; }
    
    public OrderStatus Status { get; set; }
    
    public DateTime OrderDate { get; set; }
    
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
