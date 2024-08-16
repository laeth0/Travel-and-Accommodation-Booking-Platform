using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Messages;
using Booking.PL.CustomExceptions;
using MediatR;

namespace Booking.Application.Mediatr;
public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryService _cloudinaryService;

    public DeleteCityCommandHandler(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
    {
        _unitOfWork = unitOfWork;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<Unit> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
    {
        var city = await _unitOfWork.CityRepository.FindAsync(request.CityId, cancellationToken)
            ?? throw new NotFoundException(CityMessages.NotFound);

        _unitOfWork.CityRepository.Delete(city);
        await _cloudinaryService.DeleteImageAsync(city.ImagePublicId);

        return Unit.Value;

    }


}