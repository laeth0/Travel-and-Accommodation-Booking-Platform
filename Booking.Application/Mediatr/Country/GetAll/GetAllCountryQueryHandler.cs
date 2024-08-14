


using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using MediatR;

namespace Booking.Application.Mediatr;
public class GetAllCountryQueryHandler : IRequestHandler<GetAllCountryQuery, IReadOnlyList<CountryResponse>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllCountryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<CountryResponse>> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
    {
        var countries = await _unitOfWork.CountryRepository.GetAllAsync(cancellationToken: cancellationToken);

        var mappedCountries = _mapper.Map<IReadOnlyList<CountryResponse>>(countries);

        return mappedCountries;
    }
}
