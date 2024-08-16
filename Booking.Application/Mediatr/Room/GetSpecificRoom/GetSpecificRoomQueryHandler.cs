



using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Messages;
using Booking.PL.CustomExceptions;
using MediatR;

namespace Booking.Application.Mediatr;

public class GetSpecificRoomQueryHandler : IRequestHandler<GetSpecificRoomQuery, RoomResponse>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSpecificRoomQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<RoomResponse> Handle(GetSpecificRoomQuery request, CancellationToken cancellationToken)
    {
        var Room = await _unitOfWork.RoomRepository.FindAsync(request.Id, cancellationToken)
                                ?? throw new NotFoundException(RoomMessages.NotFound);

        return _mapper.Map<RoomResponse>(Room);

    }
}
