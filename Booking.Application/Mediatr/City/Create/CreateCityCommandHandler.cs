
using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Messages;
using Booking.PL.CustomExceptions;
using MediatR;


namespace Booking.Application.Mediatr;
public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, CityResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;


    public CreateCityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileService = fileService;
    }



    public async Task<CityResponse> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.CityRepository.IsExistAsync(c => c.Name == request.Name, cancellationToken))
            throw new BadRequestException(CityMessages.CityExist(request.Name));

        var city = _mapper.Map<City>(request);

        var createdCity = await _unitOfWork.CityRepository.AddAsync(city, cancellationToken);

        createdCity.ImageName = await _fileService.UploadFileAsync(request.Image) ?? "";

        return _mapper.Map<CityResponse>(createdCity);


    }
}