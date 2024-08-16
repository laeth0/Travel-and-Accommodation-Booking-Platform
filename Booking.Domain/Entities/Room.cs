

namespace Booking.Domain.Entities;
public class Room : BaseEntity
{
    public int AdultsCapacity { get; set; }
    public int ChildrenCapacity { get; set; }
    public int Number { get; set; }
    public int PricePerNight { get; set; }
    public string Description { get; set; }
    public string ImagePublicId { get; set; }
    public string ImageUrl { get; set; }
    public int Rating { get; set; }

    public Guid ResidenceId { get; set; }
    public virtual Residence Residence { get; set; }

    public Guid RoomTypeId { get; set; }
    public virtual RoomType RoomType { get; set; }



    public virtual ICollection<RoomBooking> RoomBookings { get; set; } = new HashSet<RoomBooking>();
    public virtual ICollection<Discount> Discounts { get; set; } = new HashSet<Discount>();
    public virtual ICollection<RoomAmenity> RoomAmenities { get; set; } = new HashSet<RoomAmenity>();


}
