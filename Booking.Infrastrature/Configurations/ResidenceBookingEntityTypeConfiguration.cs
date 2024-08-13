



using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastrature.Configurations;
public class ResidenceBookingEntityTypeConfiguration : IEntityTypeConfiguration<ResidenceBooking>
{
    public void Configure(EntityTypeBuilder<ResidenceBooking> builder)
    {
        builder.ToTable("ResidenceBookings", x =>
            x.HasCheckConstraint("CK_CorrectDates", "[CheckOutDateUtc] > [CheckInDateUtc]"));

        builder.HasKey(x => new { x.UserId, x.ResidenceId, x.Id });
        builder.Property(x => x.Id).ValueGeneratedOnAdd();


        builder.Property(x => x.TotalPrice)
            .HasPrecision(9, 2)
            .IsRequired();

        builder.Property(x => x.CheckInDateUtc).IsRequired();

        builder.Property(x => x.CheckOutDateUtc).IsRequired();

        builder.Property(x => x.GuestRemarks).IsRequired();

        builder.Property(x => x.CreatedAtUtc).IsRequired();

    }
}
