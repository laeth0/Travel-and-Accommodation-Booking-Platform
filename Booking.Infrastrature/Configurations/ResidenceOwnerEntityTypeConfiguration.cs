

using Booking.Domain.Constants;
using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastrature.Configurations;
internal class ResidenceOwnerEntityTypeConfiguration : IEntityTypeConfiguration<ResidenceOwner>
{
    public void Configure(EntityTypeBuilder<ResidenceOwner> builder)
    {
        builder.ToTable("ResidenceOwners", x =>
        {
            x.HasCheckConstraint("CK_PurchaseAndSaleDates", "[SaleDate] > [PurchaseDate]");
       
            x.HasCheckConstraint("CK_OwnershipPercentage", $"[OwnershipPercentage] >= {ResidenceOwnerConstants.OwnershipPercentageMinValue} AND [OwnershipPercentage] <= {ResidenceOwnerConstants.OwnershipPercentageMaxValue}");
        });



        builder.HasKey(e => new { e.UserId, e.ResidenceId, e.Id });
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.PurchaseDate).IsRequired();


        builder.Property(e => e.CreatedAtUtc).IsRequired();


        builder.Property(e => e.OwnershipPercentage).IsRequired();

    }
}
