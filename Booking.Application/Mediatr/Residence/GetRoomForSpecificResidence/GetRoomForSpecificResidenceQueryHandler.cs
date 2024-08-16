



using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using Booking.PL.CustomExceptions;
using MediatR;

namespace Booking.Application.Mediatr;

public class GetRoomForSpecificResidenceQueryHandler : IRequestHandler<GetRoomForSpecificResidenceQuery, IReadOnlyList<RoomResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public GetRoomForSpecificResidenceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<RoomResponse>> Handle(GetRoomForSpecificResidenceQuery request, CancellationToken cancellationToken)
    {
        var Residence = await _unitOfWork.ResidenceRepository.FindAsync(request.Id, cancellationToken) ?? throw new NotFoundException("Residence not found");

        var Rooms = Residence.Rooms.Select(c => _mapper.Map<RoomResponse>(c)).ToList();

        return Rooms;
    }
}
