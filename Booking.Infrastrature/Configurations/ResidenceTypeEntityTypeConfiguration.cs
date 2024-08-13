



using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastrature.Configurations;
internal class ResidenceTypeEntityTypeConfiguration : IEntityTypeConfiguration<ResidenceType>
{
    public void Configure(EntityTypeBuilder<ResidenceType> builder)
    {
        builder.ToTable("ResidenceTypes");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

        builder.Property(x => x.Description).HasMaxLength(200);

        builder.HasMany(x => x.Residences)
            .WithOne(x => x.ResidenceType)
            .HasForeignKey(x => x.ResidenceTypeId)
            .OnDelete(DeleteBehavior.Restrict);


    }
}
