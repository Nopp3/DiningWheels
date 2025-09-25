using DiningWheels.Domain.Common;

namespace DiningWheels.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; }
    
    public Guid MenuItemId { get; set; }
    public virtual MenuItem MenuItem { get; set; }
    
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
