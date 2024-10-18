using TravelAccommodationBookingPlatform.Domain.Enums;

namespace TravelAccommodationBookingPlatform.Domain.Shared.ValidationResult;
public interface IValidationResult
{
    public static readonly Error ValidationError = new(
        ErrorType.UnprocessableEntity,
        "ValidationError",
        "A validation problem occurred.");

    Error[] Errors { get; }
}