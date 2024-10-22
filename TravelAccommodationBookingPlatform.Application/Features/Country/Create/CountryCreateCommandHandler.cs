using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;


namespace TravelAccommodationBookingPlatform.Application.Features.Country.Create;
internal sealed class CountryCreateCommandHandler : ICommandHandler<CountryCreateCommand, CountryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CountryCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<CountryResponse>> Handle(CountryCreateCommand request, CancellationToken cancellationToken)
    {
        var country = _mapper.Map<Domain.Entities.Country>(request);
        var countryCreated = await _unitOfWork.CountryRepository.AddAsync(country, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CountryResponse>(countryCreated);
    }
}
