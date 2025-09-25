using DiningWheels.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiningWheels.Persistance.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.OwnsOne(x => x.Location, location =>
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

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.PasswordHash)
            .IsRequired();

    }
}