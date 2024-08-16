

using Booking.Domain.Constants;
using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastrature.Configurations;
internal class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("ApplicationUser");

        builder.Property(e => e.FirstName)
            .HasMaxLength(UserConstants.FirstNameMaxLength)
            .IsRequired();

        builder.Property(e => e.LastName)
            .HasMaxLength(UserConstants.LastNameMaxLength)
            .IsRequired();

        builder.HasMany(e => e.ResidenceOwners)
           .WithOne(e => e.User)
           .HasForeignKey(e => e.UserId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.RoomBookings)
            .WithOne(e => e.User)
              .HasForeignKey(e => e.UserId)
              .OnDelete(DeleteBehavior.Cascade);



    }
}
