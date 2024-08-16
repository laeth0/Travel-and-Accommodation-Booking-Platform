



using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastrature.Configurations;
internal class RoomBookingEntityTypeConfiguration : IEntityTypeConfiguration<RoomBooking>
{
    public void Configure(EntityTypeBuilder<RoomBooking> builder)
    {
        builder.ToTable("RoomBookings");

        builder.HasKey(rb => rb.Id);
        builder.Property(rb => rb.Id).ValueGeneratedOnAdd();


        builder.Property(rb => rb.TotalPrice).IsRequired();
        builder.Property(rb => rb.CheckInDateUtc).IsRequired();
        builder.Property(rb => rb.CheckOutDateUtc).IsRequired();
        builder.Property(rb => rb.UserRemarks).IsRequired(false);


    }
}
