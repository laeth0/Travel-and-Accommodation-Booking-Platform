

using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Messages;
using Booking.PL.CustomExceptions;
using MediatR;

namespace Booking.Application.Mediatr;

public class GetSpecificCityQueryHandler : IRequestHandler<GetSpecificCityQuery, CityResponse>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSpecificCityQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CityResponse> Handle(GetSpecificCityQuery request, CancellationToken cancellationToken)
    {
        var city = await _unitOfWork.CityRepository.FindAsync(request.Id, cancellationToken)
                                ?? throw new NotFoundException(CityMessages.CityNotExist());

        return _mapper.Map<CityResponse>(city);

    }
}
