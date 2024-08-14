using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Messages;
using Booking.PL.CustomExceptions;
using MediatR;


namespace Booking.Application.Mediatr;
public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, CityResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;

    public UpdateCityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
    {

        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileService = fileService;
    }


    public async Task<CityResponse> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
    {
        var City = await _unitOfWork.CityRepository.FindAsync(request.Id, cancellationToken) ??
                         throw new NotFoundException(CityMessages.NotFound);

        if (await _unitOfWork.CityRepository.IsExistAsync(c => c.Name == request.Name, cancellationToken))
            throw new BadRequestException(CityMessages.CityExist(request.Name));




        _mapper.Map(request, City);

        _unitOfWork.CityRepository.Update(City);

        _fileService.DeleteFile(City.ImageName);

        City.ImageName = await _fileService.UploadFileAsync(request.Image) ?? "";


        return _mapper.Map<CityResponse>(City);
    }
}