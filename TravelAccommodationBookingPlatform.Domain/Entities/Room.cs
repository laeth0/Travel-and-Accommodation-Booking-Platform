using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Domain.Entities;
public class Room : BaseEntity
{
    public int RoomNumber { get; set; }

    public Price PricePerNight { get; set; }

    public StarRate StarRate { get; set; }

    public NumberOfGuests MaxNumberOfGuests { get; set; }

    public Guid HotelId { get; set; }

    public virtual Hotel Hotel { get; set; }

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}
