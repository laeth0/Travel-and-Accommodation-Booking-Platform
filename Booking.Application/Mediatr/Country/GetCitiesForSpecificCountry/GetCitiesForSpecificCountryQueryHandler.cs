




using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using Booking.PL.CustomExceptions;
using MediatR;

namespace Booking.Application.Mediatr;
public class GetCitiesForSpecificCountryQueryHandler : IRequestHandler<GetCitiesForSpecificCountryQuery, IReadOnlyList<CityResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCitiesForSpecificCountryQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<CityResponse>> Handle(GetCitiesForSpecificCountryQuery request, CancellationToken cancellationToken)
    {
        var country = await _unitOfWork.CountryRepository.FindAsync(request.CountryId, cancellationToken) ?? throw new NotFoundException("Country not found");

        var cities = country.Cities.Select(c => _mapper.Map<CityResponse>(c)).ToList();

        return cities;

    }
}