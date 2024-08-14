


using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using MediatR;

namespace Booking.Application.Mediatr;
public class GetAllCityQueryHandler : IRequestHandler<GetAllCityQuery, IReadOnlyList<CityResponse>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllCityQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<CityResponse>> Handle(GetAllCityQuery request, CancellationToken cancellationToken)
    {
        var Cities = await _unitOfWork.CityRepository.GetAllAsync(cancellationToken: cancellationToken);

        return _mapper.Map<IReadOnlyList<CityResponse>>(Cities);


    }
}
