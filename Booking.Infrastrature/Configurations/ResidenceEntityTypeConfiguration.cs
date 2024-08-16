




using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastrature.Configurations;
internal class ResidenceEntityTypeConfiguration : IEntityTypeConfiguration<Residence>
{
    public void Configure(EntityTypeBuilder<Residence> builder)
    {
        builder.ToTable("Residences");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.Name).IsRequired();

        builder.Property(e => e.Description).IsRequired();

        builder.Property(e => e.Address).IsRequired();



        builder.Property(e => e.Email)
            .HasAnnotation("EmailAddress", true);

        builder.Property(e => e.PhoneNumber)
            .HasAnnotation("Phone", true);


        builder.Property(e => e.FloorsNumber).IsRequired();

        builder.Property(e => e.Rating).IsRequired();


        builder.HasMany(e => e.Rooms)
            .WithOne(e => e.Residence)
            .HasForeignKey(e => e.ResidenceId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasMany(e => e.ResidenceOwners)
            .WithOne(e => e.Residence)
            .HasForeignKey(e => e.ResidenceId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
