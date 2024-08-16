
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
    private readonly ICloudinaryService _cloudinaryService;


    public CreateCityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICloudinaryService cloudinaryService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }



    public async Task<CityResponse> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.CityRepository.IsExistAsync(c => c.Name == request.Name, cancellationToken))
            throw new BadRequestException(CityMessages.CityExist(request.Name));

        var city = _mapper.Map<City>(request);

        var createdCity = await _unitOfWork.CityRepository.AddAsync(city, cancellationToken);

        (createdCity.ImagePublicId, createdCity.ImageUrl) = await _cloudinaryService.UploadImageAsync(request.Image, cancellationToken);

        return _mapper.Map<CityResponse>(createdCity);


    }
}