using DiningWheels.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiningWheels.Persistance.Configuration;

public class OpeningHoursConfiguration : IEntityTypeConfiguration<OpeningHours>
{
    public void Configure(EntityTypeBuilder<OpeningHours> builder)
    {
        builder.HasKey(oh => oh.Id);
        
        builder.HasOne(x => x.Restaurant)
            .WithMany(x => x.OpeningHours)
            .HasForeignKey(x => x.RestaurantId);
        
        builder.HasIndex(x => new { x.RestaurantId, x.DayOfWeek })
            .IsUnique();

    }
}