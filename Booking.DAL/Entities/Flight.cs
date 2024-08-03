
namespace Booking.DAL.Entities;

public class Flight
{
    public Guid Id { get; set; }
    public string DepartureCity { get; set; } = null!;
    public string ArrivalCity { get; set; } = null!;
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public int Price { get; set; }
    public int AvailableSeats { get; set; }
    public ICollection<UserFlight> UserFlight { get; set; } = new HashSet<UserFlight>();
}
