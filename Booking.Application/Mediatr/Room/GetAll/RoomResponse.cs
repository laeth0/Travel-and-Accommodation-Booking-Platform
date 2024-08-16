


namespace Booking.Application.Mediatr;
public class RoomResponse
{
    public Guid Id { get; set; }
    public int AdultsCapacity { get; set; }
    public int ChildrenCapacity { get; set; }
    public int Number { get; set; }
    public int PricePerNight { get; set; }
    public string Description { get; set; }
    public string ImagePublicId { get; set; }
    public string ImageUrl { get; set; }
    public int Rating { get; set; }

    public Guid ResidenceId { get; set; }
    public string Residence { get; set; }

    public Guid RoomTypeId { get; set; }
    public string RoomType { get; set; }
}
