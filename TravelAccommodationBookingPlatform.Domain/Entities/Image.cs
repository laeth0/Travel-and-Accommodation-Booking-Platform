using TravelAccommodationBookingPlatform.Domain.Enums;

namespace TravelAccommodationBookingPlatform.Domain.Entities;
public class Image : BaseEntity
{

    public string Url { get; set; }

    public ImageType Type { get; set; }
}
