using Bogus;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Persistence.DataSeed.Generators;
internal static class HotelDataGenerator
{
    public static List<Hotel> GenerateHotels(int numberOfHotels = 5)
    {
        var faker = new Faker();
        var hotels = new List<Hotel>();

        for (int i = 0; i < numberOfHotels; i++)
        {
            var hotel = new Hotel
            {
                Name = faker.Company.CompanyName(),
                Description = faker.Lorem.Paragraph(10),
                Coordinates = new Coordinates(faker.Address.Latitude(), faker.Address.Longitude()),
                StarRate = (StarRate)faker.Random.Number(1, 5),
                Rooms = RoomDataGenerator.GenerateRooms(),
                Reviews = new List<Review>(),
                Images = ImageDataGenerator.GenerateImages()
            };

            hotels.Add(hotel);
        }

        return hotels;
    }
}
