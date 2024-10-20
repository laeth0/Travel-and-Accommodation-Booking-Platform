using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations;
public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(user => user.UserName)
            .HasMaxLength(DomainRules.Users.UsernameMaxLength)
            .IsRequired();

        builder.Property(user => user.Email)
            .HasMaxLength(DomainRules.Users.EmailMaxLength)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();


        builder.OwnsOne(
            user => user.ActivationCode,
            activationCode =>
            {
                activationCode.WithOwner();

                activationCode.Property(x => x.Value)
                    .HasColumnName("ActivationCode");

                activationCode.Property(x => x.ExpiresAtUtc)
                    .HasColumnName("ActivationCodeExpiresAt");
            }
        );

        builder.HasMany(u => u.Tokens)
        .WithOne(t => t.AppUser)
        .HasForeignKey(t => t.AppUserId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(user => user.Reviews)
            .WithOne(review => review.AppUser)
            .HasForeignKey(review => review.AppUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(user => user.Bookings)
            .WithOne(booking => booking.AppUser)
            .HasForeignKey(booking => booking.AppUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
