


using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Messages;
using Booking.PL.CustomExceptions;
using MediatR;

namespace Booking.Application.Mediatr;
public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryService _cloudinaryService;

    public DeleteRoomCommandHandler(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
    {
        _unitOfWork = unitOfWork;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<Unit> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var Room = await _unitOfWork.RoomRepository.FindAsync(request.RoomId, cancellationToken)
          ?? throw new NotFoundException(RoomMessages.NotFound);

        _unitOfWork.RoomRepository.Delete(Room);
        await _cloudinaryService.DeleteImageAsync(Room.ImagePublicId);

        return Unit.Value;
    }
}
