using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Persistence.Configurations.Associations;
using TravelAccommodationBookingPlatform.Persistence.Configurations.PropertyBuilderExtensions;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations;
public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.RoomNumber)
            .IsRequired()
            .HasAnnotation("Min", DomainRules.Rooms.RoomNumberMin)
            .HasAnnotation("Max", DomainRules.Rooms.RoomNumberMax);

        builder.ComplexProperty(r => r.PricePerNight)
            .ApplyPriceConfiguration();

        builder.Property(r => r.StarRate)
            .IsRequired();

        builder.ComplexProperty(r => r.MaxNumberOfGuests)
            .ApplyNumberOfGuestsConfiguration();

        builder.HasMany(r => r.Discounts)
            .WithOne(d => d.Room)
            .HasForeignKey(d => d.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.Images)
            .WithMany()
            .UsingEntity<RoomImageAssociation>(
                j => j
                    .HasOne(ia => ia.Image)
                    .WithMany()
                    .HasForeignKey(ia => ia.ImageId)
                    .OnDelete(DeleteBehavior.NoAction),
                j => j
                    .HasOne<Room>()
                    .WithMany()
                    .HasForeignKey(ia => ia.RoomId)
                    .OnDelete(DeleteBehavior.NoAction));


    }
}
