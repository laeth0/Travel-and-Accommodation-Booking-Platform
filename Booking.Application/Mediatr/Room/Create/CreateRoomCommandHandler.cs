

using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Services;
using MediatR;

namespace Booking.Application.Mediatr;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, RoomResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryService _cloudinaryService;


    public CreateRoomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICloudinaryService cloudinaryService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<RoomResponse> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {

        var room = _mapper.Map<Room>(request);

        var createdRoom = await _unitOfWork.RoomRepository.AddAsync(room, cancellationToken);

        (createdRoom.ImagePublicId, createdRoom.ImageUrl) = await _cloudinaryService.UploadImageAsync(request.Image, cancellationToken);

        return _mapper.Map<RoomResponse>(createdRoom);

    }
}
