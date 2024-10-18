using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations;
internal class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Code)
            .IsRequired();

        builder.Property(d => d.Name)
            .HasMaxLength(DomainRules.Countries.NameMaxLength)
            .IsRequired();

        builder.Property(d => d.Description)
             .HasMaxLength(DomainRules.Countries.DescriptionMaxLength)
             .IsRequired();

        builder.Property(d => d.Percentage)
            .HasAnnotation("Min", DomainRules.Discount.PercentageMin)
            .HasAnnotation("Max", DomainRules.Discount.PercentageMax)
            .IsRequired();

    }
}
