


using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastrature.Configurations;
internal class RoomAmenityEntityTypeConfiguration : IEntityTypeConfiguration<RoomAmenity>
{
    public void Configure(EntityTypeBuilder<RoomAmenity> builder)
    {

        builder.ToTable("RoomAmenities");

        builder.HasKey(e => new { e.RoomId, e.AmenityId, e.Id });
        builder.Property(e => e.Id).ValueGeneratedOnAdd();





    }
}
