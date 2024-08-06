

using Booking.DAL.Validators;
using Microsoft.AspNetCore.Identity;

namespace Booking.DAL.Entities;
public class ApplicationUser : IdentityUser
{
    [CapitalizeCheck(ErrorMessage = "First Name must start with a capital letter.")]
    public string FirstName { get; set; } 

    [CapitalizeCheck(ErrorMessage = "Last Name must start with a capital letter.")]
    public string LastName { get; set; } 
    public string? ImageName { get; set; } 

    public virtual ICollection<RoomBooking> GuestRooms { get; set; } = new HashSet<RoomBooking>();

    public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

    public virtual ICollection<Residence> Residences { get; set; } = new HashSet<Residence>();

    public virtual ICollection<UserFlight> UserFlights { get; set; } = new HashSet<UserFlight>();

}
