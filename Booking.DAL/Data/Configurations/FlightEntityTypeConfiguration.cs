


using Booking.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Data.Configurations;
internal class FlightEntityTypeConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id).ValueGeneratedOnAdd();

        builder.Property(f => f.DepartureCity).IsRequired();

        builder.Property(f => f.ArrivalCity).IsRequired();

        builder.Property(f => f.DepartureTime).IsRequired();

        builder.Property(f => f.ArrivalTime).IsRequired();

        builder.Property(f => f.Price).IsRequired();

        builder.Property(f => f.AvailableSeats).IsRequired();

        builder.HasMany(f => f.UserFlight)
            .WithOne(uf => uf.Flight)
            .HasForeignKey(uf => uf.FlightId);
    }
}
