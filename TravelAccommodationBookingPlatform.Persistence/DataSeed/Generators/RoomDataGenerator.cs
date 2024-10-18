using Bogus;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Persistence.DataSeed.Generators;
internal static class RoomDataGenerator
{
    public static List<Room> GenerateRooms(int numberOfRooms = 5)
    {
        var faker = new Faker();
        var rooms = new List<Room>();

        for (int i = 0; i < numberOfRooms; i++)
        {
            var room = new Room
            {
                RoomNumber = faker.Random.Number(1, 100),
                PricePerNight = new Price() { Value = faker.Random.Double(50, 500) },
                StarRate = (StarRate)faker.Random.Number(1, 5),
                MaxNumberOfGuests = new NumberOfGuests(faker.Random.Int(1, 7), faker.Random.Int(1, 7)),
                Images = ImageDataGenerator.GenerateImages()
            };

            rooms.Add(room);
        }

        return rooms;
    }
}
