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
    private readonly ICloudinaryService _cloudinaryService;

    public UpdateCityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICloudinaryService cloudinaryService)
    {

        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }


    public async Task<CityResponse> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
    {
        var City = await _unitOfWork.CityRepository.FindAsync(request.Id, cancellationToken) ??
                         throw new NotFoundException(CityMessages.NotFound);

        if (await _unitOfWork.CityRepository.IsExistAsync(c => c.Name == request.Name, cancellationToken))
            throw new BadRequestException(CityMessages.CityExist(request.Name));


        _mapper.Map(request, City);

        _unitOfWork.CityRepository.Update(City);

        (City.ImagePublicId, City.ImageUrl) = await _cloudinaryService.ReplaceImageAsync(City.ImagePublicId, request.Image, cancellationToken);

        return _mapper.Map<CityResponse>(City);
    }
}