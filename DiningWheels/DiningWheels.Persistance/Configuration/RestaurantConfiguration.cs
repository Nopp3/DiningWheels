using DiningWheels.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiningWheels.Persistance.Configuration;

public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Location, location =>
        {
            location.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(500);
            location.Property(x => x.Latitude) 
                .IsRequired();
            location.Property(x => x.Longitude) 
                .IsRequired();
        });
    }
}