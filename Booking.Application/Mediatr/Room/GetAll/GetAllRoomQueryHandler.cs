



using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using MediatR;

namespace Booking.Application.Mediatr;
public class GetAllRoomQueryHandler : IRequestHandler<GetAllRoomQuery, IReadOnlyList<RoomResponse>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllRoomQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<RoomResponse>> Handle(GetAllRoomQuery request, CancellationToken cancellationToken)
    {
        var Rooms = await _unitOfWork.RoomRepository.GetAllAsync(cancellationToken: cancellationToken);

        return _mapper.Map<IReadOnlyList<RoomResponse>>(Rooms);


    }
}
