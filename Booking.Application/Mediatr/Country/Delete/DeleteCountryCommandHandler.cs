using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Messages;
using Booking.PL.CustomExceptions;
using MediatR;

namespace Booking.Application.Mediatr;
public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryService _cloudinaryService;

    public DeleteCountryCommandHandler(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
    {
        _unitOfWork = unitOfWork;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await _unitOfWork.CountryRepository.FindAsync(request.CountryId, cancellationToken)
            ?? throw new NotFoundException(CityMessages.NotFound);

        //if (await _hotelRepository.ExistsAsync(h => h.CityId == request.CityId, cancellationToken))
        //    throw new DependentsExistException(CityMessages.DependentsExist);

        _unitOfWork.CountryRepository.Delete(country);
        await _cloudinaryService.DeleteImageAsync(country.ImagePublicId);

        return Unit.Value;

    }


}