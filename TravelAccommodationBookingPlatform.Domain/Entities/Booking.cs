using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Domain.Entities;
public class Booking : BaseEntity, IAuditableEntity
{

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? SpecialRequest { get; set; }

    public Checking Checking { get; set; }

    public NumberOfGuests NumberOfGuests { get; set; }

    public double TotalPrice { get; set; }

    public Guid PaymentId { get; set; }

    public virtual Payment Payment { get; set; }

    public string AppUserId { get; set; }

    public virtual AppUser AppUser { get; set; }

    public Guid RoomId { get; set; }

    public virtual Room Room { get; set; }




}

