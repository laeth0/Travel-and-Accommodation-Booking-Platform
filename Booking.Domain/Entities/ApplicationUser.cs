

using Microsoft.AspNetCore.Identity;

namespace Booking.Domain.Entities;
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string? ImagePublicId { get; set; }
    public string? ImageUrl { get; set; }

    public virtual ICollection<ResidenceOwner> ResidenceOwners { get; set; } = new HashSet<ResidenceOwner>();

    public virtual ICollection<RoomBooking> RoomBookings{ get; set; } = new HashSet<RoomBooking>();


}
