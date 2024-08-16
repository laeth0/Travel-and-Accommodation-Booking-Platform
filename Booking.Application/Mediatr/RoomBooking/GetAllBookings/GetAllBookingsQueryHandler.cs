


using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using MediatR;

namespace Booking.Application.Mediatr;
public class GetAllBookingsQueryHandler : IRequestHandler<GetAllBookingsQuery, IReadOnlyList<RoomBookingResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllBookingsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<RoomBookingResponse>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
    {
        var bookings = await _unitOfWork.RoomBookingRepository.GetAllAsync(filterCondition: x => x.UserId == request.UserId, cancellationToken: cancellationToken);


        return _mapper.Map<IReadOnlyList<RoomBookingResponse>>(bookings);
    }
}
