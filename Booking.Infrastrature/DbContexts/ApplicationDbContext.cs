


using Booking.Domain.Entities;
using Booking.Infrastrature.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastrature.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            // It is possible to apply all configuration specified in types implementing IEntityTypeConfiguration in a given assembly.
            builder.ApplyConfigurationsFromAssembly(typeof(CityEntityTypeConfiguration).Assembly);

            base.OnModelCreating(builder);// This is important => we should call the base method to avoid the error
        }


        /*
        if i Dont Use this Objects I Can't write _context.Cities.toList() in the controller
        i Should write _context.Set<City>().toList() instead (this used in fluent api)
        public DbSet<City> Cities { get; set; }
        */


    }
}
