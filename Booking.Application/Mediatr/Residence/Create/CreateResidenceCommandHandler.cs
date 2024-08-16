


using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Services;
using MediatR;

namespace Booking.Application.Mediatr;
public class CreateResidenceCommandHandler : IRequestHandler<CreateResidenceCommand, ResidenceResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryService _cloudinaryService;


    public CreateResidenceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICloudinaryService cloudinaryService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<ResidenceResponse> Handle(CreateResidenceCommand request, CancellationToken cancellationToken)
    {

        var Residence = _mapper.Map<Residence>(request);

        var createdResidence = await _unitOfWork.ResidenceRepository.AddAsync(Residence, cancellationToken);

        (createdResidence.ImagePublicId, createdResidence.ImageUrl) = await _cloudinaryService.UploadImageAsync(request.Image, cancellationToken);

        return _mapper.Map<ResidenceResponse>(createdResidence);

    }
}
