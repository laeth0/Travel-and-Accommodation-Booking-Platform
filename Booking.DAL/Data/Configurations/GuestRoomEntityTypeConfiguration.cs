



using Booking.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Data.Configurations;

internal class GuestRoomEntityTypeConfiguration : IEntityTypeConfiguration<GuestRoom>
{
    public void Configure(EntityTypeBuilder<GuestRoom> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.CheckIn).IsRequired();

        builder.Property(e => e.CheckOut).IsRequired();

        builder.Property(e => e.TotalPrice).IsRequired();

        builder.Property(e => e.CreatedAt).IsRequired();

        builder.Property(e => e.UpdatedAt).IsRequired();

        builder.HasMany(e => e.Reviews)
            .WithOne(e => e.GuestRoom)
            .HasForeignKey(e => e.GuestRoomId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
