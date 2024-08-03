﻿

namespace Booking.DAL.ConfigModels;

public class JWT
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int DurationInDays { get; set; }
}
