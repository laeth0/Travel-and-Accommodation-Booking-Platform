



using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastrature.Configurations;
internal class RoomBookingEntityTypeConfiguration : IEntityTypeConfiguration<RoomBooking>
{
    public void Configure(EntityTypeBuilder<RoomBooking> builder)
    {
        builder.ToTable("RoomBookings");

        builder.HasKey(e => new { e.RoomId, e.ResidenceBookingId, e.Id });
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

    }
}
