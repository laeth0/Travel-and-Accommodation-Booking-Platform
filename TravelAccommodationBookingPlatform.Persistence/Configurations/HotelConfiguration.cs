using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Persistence.Configurations.Associations;
using TravelAccommodationBookingPlatform.Persistence.Configurations.PropertyBuilderExtensions;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations;
internal class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(d => d.Name)
            .HasMaxLength(DomainRules.Hotels.NameMaxLength)
            .IsRequired();

        builder.Property(d => d.Description)
             .HasMaxLength(DomainRules.Hotels.DescriptionMaxLength)
             .IsRequired();

        builder.ComplexProperty(h => h.Coordinates)
            .ApplyCoordinatesConfiguration();

        builder.Property(h => h.StarRate)
            .IsRequired();

        builder.HasMany(h => h.Rooms)
            .WithOne(r => r.Hotel)
            .HasForeignKey(r => r.HotelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(h => h.Reviews)
            .WithOne(r => r.Hotel)
            .HasForeignKey(r => r.HotelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(h => h.Images)
            .WithMany()
            .UsingEntity<HotelImageAssociation>(
                j => j
                    .HasOne(ia => ia.Image)
                    .WithMany()
                    .HasForeignKey(ia => ia.ImageId)
                    .OnDelete(DeleteBehavior.NoAction),
                j => j
                    .HasOne<Hotel>()
                    .WithMany()
                    .HasForeignKey(ia => ia.HotelId)
                    .OnDelete(DeleteBehavior.NoAction)
                    );

    }
}
