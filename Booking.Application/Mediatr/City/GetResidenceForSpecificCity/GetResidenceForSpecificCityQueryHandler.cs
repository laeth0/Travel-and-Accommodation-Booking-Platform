

using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using Booking.PL.CustomExceptions;
using MediatR;

namespace Booking.Application.Mediatr;
public class GetResidenceForSpecificCityQueryHandler : IRequestHandler<GetResidenceForSpecificCityQuery, IReadOnlyList<ResidenceResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetResidenceForSpecificCityQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<ResidenceResponse>> Handle(GetResidenceForSpecificCityQuery request, CancellationToken cancellationToken)
    {
        var City = await _unitOfWork.CityRepository.FindAsync(request.CityId, cancellationToken) ?? throw new NotFoundException("City not found");

        var residences = City.Residences.Select(x => _mapper.Map<ResidenceResponse>(x)).ToList();

        return residences;
    }
}
