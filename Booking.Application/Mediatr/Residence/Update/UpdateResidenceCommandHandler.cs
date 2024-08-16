



using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Messages;
using Booking.PL.CustomExceptions;
using MediatR;

namespace Booking.Application.Mediatr;

public class UpdateResidenceCommandHandler : IRequestHandler<UpdateResidenceCommand, ResidenceResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryService _cloudinaryService;

    public UpdateResidenceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICloudinaryService cloudinaryService)
    {

        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }


    public async Task<ResidenceResponse> Handle(UpdateResidenceCommand request, CancellationToken cancellationToken)
    {
        var Residence = await _unitOfWork.ResidenceRepository.FindAsync(request.Id, cancellationToken) ??
                         throw new NotFoundException(ResidenceMessages.NotFound);

        if (await _unitOfWork.ResidenceRepository.IsExistAsync(c => c.Name == request.Name, cancellationToken))
            throw new BadRequestException(ResidenceMessages.ResidenceExist(request.Name));

        _mapper.Map(request, Residence);

        _unitOfWork.ResidenceRepository.Update(Residence);

        (Residence.ImagePublicId, Residence.ImageUrl) = await _cloudinaryService.ReplaceImageAsync(Residence.ImagePublicId, request.Image, cancellationToken);

        return _mapper.Map<ResidenceResponse>(Residence);
    }
}