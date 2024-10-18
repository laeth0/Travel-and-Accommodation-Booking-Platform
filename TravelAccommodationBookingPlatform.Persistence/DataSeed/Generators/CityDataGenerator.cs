using Bogus;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.DataSeed.Generators;
internal static class CityDataGenerator
{
    public static List<City> GenerateCities(int numberOfCities = 5)
    {
        var faker = new Faker();
        var cities = new List<City>();

        for (int i = 0; i < numberOfCities; i++)
        {
            var city = new City
            {
                Name = faker.Address.City(),
                Description = faker.Lorem.Paragraph(10),
                Image = ImageDataGenerator.GenerateImages(1)[0],
                Hotels = HotelDataGenerator.GenerateHotels()
            };

            cities.Add(city);
        }

        return cities;
    }
}

