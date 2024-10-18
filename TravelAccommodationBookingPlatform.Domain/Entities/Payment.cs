using TravelAccommodationBookingPlatform.Domain.Enums;

namespace TravelAccommodationBookingPlatform.Domain.Entities;
public class Payment : BaseEntity
{

    public string Method { get; set; }

    public double Amount { get; set; }

    public string ConfirmationNumber { get; set; }

    public DateTime Date { get; set; }

    public PaymentStatus Status { get; set; }

    public Currency Currency { get; set; }

    public virtual Booking Booking { get; set; }

}
