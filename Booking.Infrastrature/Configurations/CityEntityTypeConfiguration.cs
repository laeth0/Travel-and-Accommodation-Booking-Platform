


using Booking.Domain.Constants;
using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastrature.Configurations;
internal class CityEntityTypeConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.Name).IsRequired().HasMaxLength(CityConstants.NameMaxLength);

        builder.Property(e => e.Description).IsRequired().HasMaxLength(CityConstants.DescriptionMaxLength);

        builder.HasMany(e => e.Residences)
            .WithOne(e => e.City)
            .HasForeignKey(e => e.CityId)
            .OnDelete(DeleteBehavior.Cascade);


        /*
         like when using OrderStatus enum :-
          
       builder.Property(b => b.PaymentMethod)
      .HasConversion(new EnumToStringConverter<PaymentMethod>());
         */


    }
}
