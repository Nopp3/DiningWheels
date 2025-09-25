using DiningWheels.Domain.Common;
using DiningWheels.Domain.Enums;

namespace DiningWheels.Domain.Entities;

using System;

public class ContactMessage : BaseEntity
{
    public ContactMessageType MessageType { get; set; }
    
    public string Name { get; set; }
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public DateTime DateSent { get; set; }
}
