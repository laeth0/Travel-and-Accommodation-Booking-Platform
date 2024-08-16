



using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using Booking.PL.CustomExceptions;
using MediatR;

namespace Booking.Application.Mediatr;

public class MakeRatingForBookingCommandHandler : IRequestHandler<MakeRatingForBookingCommand, RoomBookingResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MakeRatingForBookingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<RoomBookingResponse> Handle(MakeRatingForBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _unitOfWork.RoomBookingRepository.FindAsync(request.RoomBookingId)
                            ?? throw new NotFoundException("Booking not found");

        booking.Rating = request.Rating;

        return _mapper.Map<RoomBookingResponse>(booking);


    }

}
