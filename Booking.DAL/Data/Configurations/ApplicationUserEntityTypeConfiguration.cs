using Booking.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Data.Configurations;

internal class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("ApplicationUser");

        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(40);


        builder.HasMany(e => e.GuestRooms)
            .WithOne(e => e.Guest)
            .HasForeignKey(e => e.GuestId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.Reviews)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Residences)
            .WithOne(e => e.Owner)
            .HasForeignKey(e => e.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.UserFlights)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
