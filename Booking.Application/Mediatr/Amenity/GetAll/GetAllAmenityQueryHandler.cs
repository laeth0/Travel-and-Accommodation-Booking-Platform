


using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using MediatR;

namespace Booking.Application.Mediatr;
public class GetAllAmenityQueryHandler : IRequestHandler<GetAllAmenityQuery, IReadOnlyList<AmenityResponse>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllAmenityQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<AmenityResponse>> Handle(GetAllAmenityQuery request, CancellationToken cancellationToken)
    {
        var Amenities = await _unitOfWork.AmenityRepository.GetAllAsync(cancellationToken: cancellationToken);

        return _mapper.Map<IReadOnlyList<AmenityResponse>>(Amenities);


    }
}
