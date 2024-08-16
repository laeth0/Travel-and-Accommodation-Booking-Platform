



using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using MediatR;

namespace Booking.Application.Mediatr;
public class GetAllResidenceQueryHandler : IRequestHandler<GetAllResidenceQuery, IReadOnlyList<ResidenceResponse>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllResidenceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<ResidenceResponse>> Handle(GetAllResidenceQuery request, CancellationToken cancellationToken)
    {
        var Residences = await _unitOfWork.ResidenceRepository.GetAllAsync(cancellationToken: cancellationToken);

        return _mapper.Map<IReadOnlyList<ResidenceResponse>>(Residences);


    }
}
