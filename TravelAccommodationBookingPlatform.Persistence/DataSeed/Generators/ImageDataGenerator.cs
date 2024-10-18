using Bogus;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.DataSeed.Generators;
internal static class ImageDataGenerator
{
    public static List<Image> GenerateImages(int numberOfImages = 5)
    {
        var faker = new Faker();
        var images = new List<Image>();

        for (int i = 0; i < numberOfImages; i++)
        {
            var image = new Image
            {
                Url = faker.Image.PicsumUrl(),
            };

            images.Add(image);
        }

        return images;
    }
}
