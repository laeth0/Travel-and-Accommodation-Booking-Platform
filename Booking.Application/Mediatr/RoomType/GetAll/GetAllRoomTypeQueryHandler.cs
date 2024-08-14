




using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using MediatR;

namespace Booking.Application.Mediatr;
public class GetAllRoomTypeQueryHandler : IRequestHandler<GetAllRoomTypeQuery, IReadOnlyList<RoomTypeResponse>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllRoomTypeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<RoomTypeResponse>> Handle(GetAllRoomTypeQuery request, CancellationToken cancellationToken)
    {
        var RoomTypes = await _unitOfWork.RoomTypeRepository.GetAllAsync(cancellationToken: cancellationToken);

        return _mapper.Map<IReadOnlyList<RoomTypeResponse>>(RoomTypes);

    }
}
