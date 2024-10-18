using Microsoft.AspNetCore.Identity;

namespace TravelAccommodationBookingPlatform.Domain.Entities;
public class AppUser : IdentityUser
{

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

}
