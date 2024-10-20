using Microsoft.EntityFrameworkCore;
using TravelAccommodationBookingPlatform.Persistence.DataSeed.Generators;

namespace TravelAccommodationBookingPlatform.Persistence.DataSeed;
public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!await context.Countries.AnyAsync())
        {
            var countries = CountryDataGenerator.GenerateCountries();
            await context.Countries.AddRangeAsync(countries);
            await context.SaveChangesAsync();
        }

    }
}
