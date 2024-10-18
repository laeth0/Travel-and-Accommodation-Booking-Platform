using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Persistence.Configurations.PropertyBuilderExtensions;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations;
public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();

        builder.Property(b => b.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAddOrUpdate();

        builder.Property(b => b.SpecialRequest)
            .HasMaxLength(DomainRules.Booking.SpecialRequestMaxLength);

        builder.ComplexProperty(b => b.Checking)
            .ApplyCheckingConfiguration();

        builder.ComplexProperty(b => b.NumberOfGuests)
                .ApplyNumberOfGuestsConfiguration();

        builder.Property(b => b.TotalPrice)
            .IsRequired();

        builder.HasOne(b => b.Payment)
          .WithOne(p => p.Booking)
          .HasForeignKey<Booking>(b => b.PaymentId)
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

    }
}
