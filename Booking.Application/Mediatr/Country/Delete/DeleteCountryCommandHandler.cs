using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Messages;
using Booking.PL.CustomExceptions;
using MediatR;

namespace Booking.Application.Mediatr;
public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;

    public DeleteCountryCommandHandler(IUnitOfWork unitOfWork, IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await _unitOfWork.CountryRepository.FindAsync(request.CountryId, cancellationToken)
            ?? throw new NotFoundException(CityMessages.NotFound);

        //if (await _hotelRepository.ExistsAsync(h => h.CityId == request.CityId, cancellationToken))
        //    throw new DependentsExistException(CityMessages.DependentsExist);

        _unitOfWork.CountryRepository.Delete(country);
        _fileService.DeleteFile(country.ImageName);

        return Unit.Value;

    }


}