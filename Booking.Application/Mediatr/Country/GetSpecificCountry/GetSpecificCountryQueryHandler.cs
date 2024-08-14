

using AutoMapper;
using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Messages;
using Booking.PL.CustomExceptions;
using MediatR;

namespace Booking.Application.Mediatr;

public class GetSpecificCountryQueryHandler : IRequestHandler<GetSpecificCountryQuery, CountryResponse>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSpecificCountryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CountryResponse> Handle(GetSpecificCountryQuery request, CancellationToken cancellationToken)
    {
        var country = await _unitOfWork.CountryRepository.FindAsync(request.Id, cancellationToken)
                                ?? throw new NotFoundException(CountryMessages.CountryNotExist());

        return _mapper.Map<CountryResponse>(country);

    }
}
