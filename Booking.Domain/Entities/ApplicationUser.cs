

using Microsoft.AspNetCore.Identity;

namespace Booking.Domain.Entities;
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ImageName { get; set; }


    public virtual ICollection<ResidenceOwner> ResidenceOwners { get; set; } = new HashSet<ResidenceOwner>();

    public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

    public virtual ICollection<ResidenceBooking> ResidenceBookings { get; set; } = new HashSet<ResidenceBooking>();


}
