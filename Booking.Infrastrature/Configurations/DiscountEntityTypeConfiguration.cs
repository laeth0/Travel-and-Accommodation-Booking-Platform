





using Booking.Domain.Constants;
using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastrature.Configurations;
public class DiscountEntityTypeConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {

        builder.ToTable("Discounts", buildAction: x =>
        {
            x.HasCheckConstraint("CK_StartAndEndDate", "[EndDateUtc] > [StartDateUtc]");

            x.HasCheckConstraint("CK_Percentage", $"[Percentage] >= {DiscountConstants.PercentageMinValue} AND [Percentage] <= {DiscountConstants.PercentageMaxValue}");
        });


        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Percentage).IsRequired();

        builder.Property(x => x.Description).IsRequired();

        builder.Property(x => x.CreatedAtUtc).IsRequired();

        builder.Property(x => x.StartDateUtc).IsRequired();

        builder.Property(x => x.EndDateUtc).IsRequired();



    }
}
