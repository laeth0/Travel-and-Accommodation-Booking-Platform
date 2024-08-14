


using Booking.Domain.Constants;
using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastrature.Configurations;
public class CountryEntityTypeConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.Name)
            .HasMaxLength(CountryConstants.NameMaxLength)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(CountryConstants.DescriptionMaxLength)
            .IsRequired();

        builder.HasMany(x => x.Cities)
            .WithOne(x => x.Country)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
