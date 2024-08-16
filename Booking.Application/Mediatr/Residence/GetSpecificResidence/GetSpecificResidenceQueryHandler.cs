



using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Messages;
using Booking.PL.CustomExceptions;
using MediatR;

namespace Booking.Application.Mediatr;
public class GetSpecificResidenceQueryHandler : IRequestHandler<GetSpecificResidenceQuery, ResidenceResponse>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSpecificResidenceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResidenceResponse> Handle(GetSpecificResidenceQuery request, CancellationToken cancellationToken)
    {
        var Residence = await _unitOfWork.ResidenceRepository.FindAsync(request.Id, cancellationToken)
                                ?? throw new NotFoundException(ResidenceMessages.NotFound);

        return _mapper.Map<ResidenceResponse>(Residence);

    }
}