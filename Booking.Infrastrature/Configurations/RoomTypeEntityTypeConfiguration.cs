


using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastrature.Configurations;
internal class RoomTypeEntityTypeConfiguration : IEntityTypeConfiguration<RoomType>
{
    public void Configure(EntityTypeBuilder<RoomType> builder)
    {

        builder.ToTable("RoomTypes");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.Name).IsRequired();

        builder.Property(e => e.Description).IsRequired();

        builder.HasMany(e => e.Rooms)
            .WithOne(e => e.RoomType)
            .HasForeignKey(e => e.RoomTypeId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
