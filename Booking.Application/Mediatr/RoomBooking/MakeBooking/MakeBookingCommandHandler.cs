



using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Entities;
using Booking.PL.CustomExceptions;
using MediatR;

namespace Booking.Application.Mediatr;

public class MakeBookingCommandHandler : IRequestHandler<MakeBookingCommand, RoomBookingResponse>
{

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public MakeBookingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<RoomBookingResponse> Handle(MakeBookingCommand request, CancellationToken cancellationToken)
    {
        var roomBooking = _mapper.Map<RoomBooking>(request);

        var room = await _unitOfWork.RoomRepository.FindAsync(request.RoomId, cancellationToken) ?? throw new NotFoundException("Room not found");


        //TimeSpan differenceTime = RoomBooking.CheckOut -  RoomBooking.CheckIn ;
        //RoomBooking.TotalPrice = differenceTime.Days * room.Price;
        roomBooking.TotalPrice = roomBooking.CheckOutDateUtc.Subtract(roomBooking.CheckInDateUtc).Days * room.PricePerNight;


        await _unitOfWork.RoomBookingRepository.AddAsync(roomBooking, cancellationToken);

        return _mapper.Map<RoomBookingResponse>(roomBooking);

    }
}

