namespace TravelAccommodationBookingPlatform.Application.Features.Country.Create;
public record CountryResponse(
    Guid Id,
    string Name,
    string Description,
    Guid? ImageId,
    string? Image);

