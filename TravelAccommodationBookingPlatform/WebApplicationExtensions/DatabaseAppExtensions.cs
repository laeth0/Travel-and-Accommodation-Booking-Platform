using Microsoft.EntityFrameworkCore;
using TravelAccommodationBookingPlatform.Persistence;
using TravelAccommodationBookingPlatform.Persistence.DataSeed;

namespace TravelAccommodationBookingPlatform.Api.WebApplicationExtensions;

public static class DatabaseAppExtensions
{
    public static async Task Migrate(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (context.Database.GetPendingMigrations().Any())
            await context.Database.MigrateAsync();
        await DataSeeder.SeedAsync(context);
    }
}
