using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TravelAccommodationBookingPlatform.Persistence;
using TravelAccommodationBookingPlatform.Persistence.DataSeed;

namespace TravelAccommodationBookingPlatform.Api.WebApplicationExtensions;

public static class DatabaseAppExtensions
{
    public static async Task Migrate(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (app.Environment.IsDevelopment())
        {
            if (dbContext.Database.GetPendingMigrations().Any())
                await dbContext.Database.MigrateAsync();

            await DataSeeder.SeedAsync(dbContext);
        }

        await RolesDataSeeding.SeedRoles(roleManager);


    }


}

