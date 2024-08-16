
using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Messages;
using Booking.PL.CustomExceptions;
using MediatR;


namespace Booking.Application.Mediatr;
public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, CountryResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryService _cloudinaryService;

    public CreateCountryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICloudinaryService cloudinaryService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<CountryResponse> Handle(CreateCountryCommand request, CancellationToken cancellationToken = default)
    {
        if (await _unitOfWork.CountryRepository.IsExistAsync(c => c.Name == request.Name, cancellationToken))
            throw new BadRequestException(CountryMessages.CountryExist(request.Name));

        var country = _mapper.Map<Country>(request);

        var createdCountry = await _unitOfWork.CountryRepository.AddAsync(country, cancellationToken);
        (createdCountry.ImagePublicId, createdCountry.ImageUrl) = await _cloudinaryService.UploadImageAsync(request.Image, cancellationToken);

        return _mapper.Map<CountryResponse>(createdCountry);
    }
}