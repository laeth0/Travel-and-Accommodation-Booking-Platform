namespace TravelAccommodationBookingPlatform.Domain.Enums;

public enum ErrorType
{
    None = 0,
    BadRequest = 1,
    NotFound = 2,
    NotAuthorized = 3,
    Conflict = 4,
    InternalServerError = 5,
    Forbidden = 6,
    TooManyRequests = 7,
    UnprocessableEntity = 8,
}
