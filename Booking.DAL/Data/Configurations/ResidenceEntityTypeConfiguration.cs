




using Booking.DAL.Entities;
using Booking.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Data.Configurations;
internal class ResidenceEntityTypeConfiguration : IEntityTypeConfiguration<Residence>
{
    public void Configure(EntityTypeBuilder<Residence> builder)
    {
        builder.ToTable("Residences");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.Email)
            .HasAnnotation("EmailAddress", true);

        builder.Property(e => e.PhoneNumber)
            .HasAnnotation("PhoneNumber", true);

        builder.HasMany(e => e.Rooms)
            .WithOne(e => e.Residence)
            .HasForeignKey(e => e.ResidenceId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
