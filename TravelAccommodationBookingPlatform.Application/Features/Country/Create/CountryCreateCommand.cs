using TravelAccommodationBookingPlatform.Application.Interfaces;

namespace TravelAccommodationBookingPlatform.Application.Features.Country.Create;
public record CountryCreateCommand(
    string Name,
    string Description) : ICommand<CountryResponse>;