using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations;
public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
           .IsRequired()
           .HasMaxLength(DomainRules.Cities.NameMaxLength);

        builder.HasIndex(c => c.Name)
            .IsUnique();


        builder.Property(c => c.Description)
            .HasMaxLength(DomainRules.Cities.DescriptionMaxLength);

        builder.HasMany(c => c.Hotels)
            .WithOne(h => h.City)
            .HasForeignKey(h => h.CityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.Image)
            .WithOne()
            .HasForeignKey<City>(c => c.ImageId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

    }
}
