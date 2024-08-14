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
    private readonly IFileService _fileService;

    public UpdateCountryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
    {

        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileService = fileService;
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

        _fileService.DeleteFile(Country.ImageName);

        Country.ImageName = await _fileService.UploadFileAsync(request.Image) ?? "";


        return _mapper.Map<CountryResponse>(Country);
    }
}