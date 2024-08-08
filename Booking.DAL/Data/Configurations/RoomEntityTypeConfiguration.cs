



using Booking.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Data.Configurations;

internal class RoomEntityTypeConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.Capacity).IsRequired();

        builder.Property(e => e.Price).IsRequired();

        builder.Property(e => e.Description).IsRequired();

        builder.HasMany(e => e.GuestRooms)
            .WithOne(e => e.Room)
            .HasForeignKey(e => e.RoomId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}
