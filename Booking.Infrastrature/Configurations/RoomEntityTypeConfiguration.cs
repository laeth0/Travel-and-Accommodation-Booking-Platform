



using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastrature.Configurations;
internal class RoomEntityTypeConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.AdultsCapacity).IsRequired();

        builder.Property(e => e.ChildrenCapacity).IsRequired();

        builder.Property(e => e.Number).IsRequired();

        builder.Property(e => e.PricePerNight).IsRequired();

        builder.Property(e => e.Description).IsRequired();


        builder.Property(e => e.Rating).IsRequired();


        builder.HasMany(e => e.RoomBookings)
            .WithOne(e => e.Room)
            .HasForeignKey(e => e.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Discounts)
            .WithOne(e => e.Room)
            .HasForeignKey(e => e.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.RoomAmenities)
            .WithOne(e => e.Room)
            .HasForeignKey(e => e.RoomId)
            .OnDelete(DeleteBehavior.Cascade);


    }
}
