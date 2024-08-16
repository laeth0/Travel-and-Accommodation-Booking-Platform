



using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using MediatR;

namespace Booking.Application.Mediatr;
public class GetAllResidenceTypeQueryHandler : IRequestHandler<GetAllResidenceTypeQuery, IReadOnlyList<ResidenceTypeResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllResidenceTypeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }



    public async Task<IReadOnlyList<ResidenceTypeResponse>> Handle(GetAllResidenceTypeQuery request, CancellationToken cancellationToken)
    {
        var ResidenceTypes = await _unitOfWork.ResidenceTypeRepository.GetAllAsync(cancellationToken: cancellationToken);

        return _mapper.Map<IReadOnlyList<ResidenceTypeResponse>>(ResidenceTypes);
    }



}
