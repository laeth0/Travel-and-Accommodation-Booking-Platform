using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Messages;
using Booking.PL.CustomExceptions;
using MediatR;

namespace Booking.Application.Mediatr;
public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;

    public DeleteCityCommandHandler(IUnitOfWork unitOfWork, IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
    {
        var city = await _unitOfWork.CityRepository.FindAsync(request.CityId, cancellationToken)
            ?? throw new NotFoundException(CityMessages.NotFound);

        _unitOfWork.CityRepository.Delete(city);
        _fileService.DeleteFile(city.ImageName);

        return Unit.Value;

    }


}