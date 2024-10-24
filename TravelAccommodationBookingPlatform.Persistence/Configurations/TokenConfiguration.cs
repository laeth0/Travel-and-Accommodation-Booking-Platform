using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations;
public class TokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Value).IsRequired();

        builder.Property(x => x.IsRevoked).IsRequired();

        builder.Property(x => x.AppUserId).IsRequired();

        builder.Property(x => x.ExpiresAt).IsRequired();

        builder.HasIndex(x => x.AppUserId);

        builder.HasIndex(x => x.Value).IsUnique();
    }
}