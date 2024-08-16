


using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Messages;
using Booking.PL.CustomExceptions;
using MediatR;

namespace Booking.Application.Mediatr;
public class DeleteResidenceCommandHandler : IRequestHandler<DeleteResidenceCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryService _cloudinaryService;

    public DeleteResidenceCommandHandler(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
    {
        _unitOfWork = unitOfWork;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<Unit> Handle(DeleteResidenceCommand request, CancellationToken cancellationToken)
    {
        var Residence = await _unitOfWork.ResidenceRepository.FindAsync(request.ResidenceId, cancellationToken)
          ?? throw new NotFoundException(ResidenceMessages.NotFound);

        _unitOfWork.ResidenceRepository.Delete(Residence);
        await _cloudinaryService.DeleteImageAsync(Residence.ImagePublicId);

        return Unit.Value;
    }
}
