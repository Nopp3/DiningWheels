using DiningWheels.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiningWheels.Persistance.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CustomerFirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.CustomerLastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.CustomerPhone)
            .IsRequired()
            .HasMaxLength(50);

        builder.OwnsOne(x => x.CustomerLocation, location =>
        {
            location.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(500);
            location.Property(x => x.Latitude)
                .IsRequired()
                .HasMaxLength(500);
            location.Property(x => x.Longitude)
                .IsRequired()
                .HasMaxLength(500);
        });
            
        
        builder.HasOne(x => x.Restaurant)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.RestaurantId);
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.UserId);

    }
}