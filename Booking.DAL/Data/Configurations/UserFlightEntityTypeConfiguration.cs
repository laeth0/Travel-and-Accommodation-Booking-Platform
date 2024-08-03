


using Booking.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Data.Configurations;
internal class UserFlightEntityTypeConfiguration : IEntityTypeConfiguration<UserFlight>
{
    public void Configure(EntityTypeBuilder<UserFlight> builder)
    {

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

    }
}
