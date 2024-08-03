

namespace Booking.DAL.Entities;

public class Residence
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string ImageName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int FloorNumber { get; set; }
    public int Rating { get; set; }

    public Guid CityId { get; set; }
    public City City { get; set; } = null!;

    public string OwnerId { get; set; } = null!;
    public ApplicationUser Owner { get; set; } = null!;


    public ICollection<Room> Rooms { get; set; } = new HashSet<Room>();
}
