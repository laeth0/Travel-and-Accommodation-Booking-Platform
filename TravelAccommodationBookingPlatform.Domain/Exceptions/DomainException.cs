using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Domain.Exceptions;
public class DomainException : Exception
{
    public DomainException(Error error) : base(error.Message) => Error = error;

    public Error Error { get; }
}

