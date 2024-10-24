using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;


namespace TravelAccommodationBookingPlatform.Application.Features.Country.Create;
internal sealed class CountryCreateCommandHandler : ICommandHandler<CountryCreateCommand, CountryResponse>
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;
    public CountryCreateCommandHandler(IMapper mapper, ICountryRepository countryRepository)
    {
        _mapper = mapper;
        _countryRepository = countryRepository;
    }

    public async Task<Result<CountryResponse>> Handle(CountryCreateCommand request, CancellationToken cancellationToken)
    {
        var country = _mapper.Map<Domain.Entities.Country>(request);
        var countryCreated = await _countryRepository.AddAsync(country, cancellationToken);
        return _mapper.Map<CountryResponse>(countryCreated);
    }
}
