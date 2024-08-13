﻿

namespace Booking.Domain.Entities;

public class Residence : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string ImageName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int FloorsNumber { get; set; }
    public float Rating { get; set; }

    public Guid CityId { get; set; }
    public virtual City City { get; set; }

    public Guid ResidenceTypeId { get; set; }
    public virtual ResidenceType ResidenceType { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new HashSet<Room>();
    public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    public virtual ICollection<ResidenceOwner> ResidenceOwners { get; set; } = new HashSet<ResidenceOwner>();
    public virtual ICollection<ResidenceBooking> ResidenceBookings { get; set; } = new HashSet<ResidenceBooking>();


}
