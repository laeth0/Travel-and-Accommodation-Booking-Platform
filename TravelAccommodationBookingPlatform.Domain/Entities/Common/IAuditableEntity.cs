﻿namespace TravelAccommodationBookingPlatform.Domain.Entities;
public interface IAuditableEntity
{
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

}
