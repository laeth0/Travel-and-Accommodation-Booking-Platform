
using Booking.DAL.Data.Configurations;
using Booking.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Booking.DAL.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        new ApplicationUserEntityTypeConfiguration().Configure(builder.Entity<ApplicationUser>()); // Other Way to do it => builder.ApplyConfigurationsFromAssembly(typeof(ProductEntityTypeConfiguration).Assembly);  
        new CityEntityTypeConfiguration().Configure(builder.Entity<City>());
        new CountryEntityTypeConfiguration().Configure(builder.Entity<Country>());
        new FlightEntityTypeConfiguration().Configure(builder.Entity<Flight>());
        new RoomBookingEntityTypeConfiguration().Configure(builder.Entity<RoomBooking>());
        new ResidenceEntityTypeConfiguration().Configure(builder.Entity<Residence>());
        new ReviewEntityTypeConfiguration().Configure(builder.Entity<Review>());
        new RoomEntityTypeConfiguration().Configure(builder.Entity<Room>());
        new UserFlightEntityTypeConfiguration().Configure(builder.Entity<UserFlight>());

        base.OnModelCreating(builder);// This is important => we should call the base method to avoid the error
    }


    // if i Dont Use this Objects I Can't write _context.Rooms.toList() in the controller
    // i Should write _context.Set<Room>().toList() instead (this used in fluent api)
    public DbSet<City> Cities { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Residence> Residences { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<RoomBooking> RoomBookings { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<UserFlight> UserFlights { get; set; }

}
