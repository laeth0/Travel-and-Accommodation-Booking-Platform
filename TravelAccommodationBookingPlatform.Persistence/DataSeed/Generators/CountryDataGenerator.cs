using Bogus;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.DataSeed.Generators;
internal static class CountryDataGenerator
{
    public static List<Country> GenerateCountries(int numberOfCountries = 5)
    {
        var faker = new Faker();
        var countries = new List<Country>();

        for (int i = 0; i < numberOfCountries; i++)
        {
            var country = new Country
            {
                Name = faker.Address.Country(),
                Description = faker.Lorem.Paragraph(10),
                Cities = CityDataGenerator.GenerateCities(),
                Image = ImageDataGenerator.GenerateImages(1)[0]
            };

            countries.Add(country);
        }

        return countries;
    }
}
