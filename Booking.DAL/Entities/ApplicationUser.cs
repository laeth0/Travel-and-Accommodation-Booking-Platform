

using Booking.DAL.Validators;
using Microsoft.AspNetCore.Identity;

namespace Booking.DAL.Entities;

public class ApplicationUser : IdentityUser
{
    [CapitalizeCheck(ErrorMessage = "First Name must start with a capital letter.")]
    public string FirstName { get; set; } = null!;

    [CapitalizeCheck(ErrorMessage = "Last Name must start with a capital letter.")]
    public string LastName { get; set; } = null!;

    public ICollection<GuestRoom> GuestRooms { get; set; } = new HashSet<GuestRoom>();

    public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

    public ICollection<Residence> Residences { get; set; } = new HashSet<Residence>();

    public ICollection<UserFlight> UserFlights { get; set; } = new HashSet<UserFlight>();

}
