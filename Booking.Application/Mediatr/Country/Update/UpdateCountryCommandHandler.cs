using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Messages;
using Booking.PL.CustomExceptions;
using MediatR;


namespace Booking.Application.Mediatr;
public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, CountryResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryService _cloudinaryService;

    public UpdateCountryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICloudinaryService cloudinaryService)
    {

        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }


    public async Task<CountryResponse> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        var Country = await _unitOfWork.CountryRepository.FindAsync(request.Id, cancellationToken) ??
                         throw new NotFoundException(CityMessages.NotFound);

        if (await _unitOfWork.CountryRepository.IsExistAsync(c => c.Name == request.Name, cancellationToken))
        {
            throw new BadRequestException(CountryMessages.CountryExist(request.Name));
        }



        _mapper.Map(request, Country);

        _unitOfWork.CountryRepository.Update(Country);

        (Country.ImagePublicId, Country.ImageUrl) = await _cloudinaryService.ReplaceImageAsync(Country.ImagePublicId, request.Image, cancellationToken);

        return _mapper.Map<CountryResponse>(Country);
    }
}