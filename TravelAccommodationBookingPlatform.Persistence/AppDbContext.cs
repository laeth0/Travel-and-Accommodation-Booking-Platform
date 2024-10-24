using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Persistence.Configurations.Associations;

namespace TravelAccommodationBookingPlatform.Persistence;
public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        base.OnModelCreating(modelBuilder);
    }



    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // 𝐆𝐥𝐨𝐛𝐚𝐥 𝐜𝐨𝐧𝐟𝐢𝐠𝐮𝐫𝐚𝐭𝐢𝐨𝐧 𝐟𝐨𝐫 𝐬𝐩𝐞𝐜𝐢𝐟𝐢𝐜 𝐭𝐲𝐩𝐞𝐬
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<string>()
            .HaveMaxLength(1000);

        configurationBuilder.Properties<DateTime>()
            .HaveColumnType("datetime");
    }



    // this make a compile time error 
    //public new DbSet<TEntity> Set<TEntity>()
    //    where TEntity : Entity => base.Set<TEntity>();

    public DbSet<Booking> Bookings { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Hotel> Hotel { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<HotelImageAssociation> HotelImageAssociations { get; set; }
    public DbSet<RoomImageAssociation> RoomImageAssociations { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

}
