using MediatR;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;

namespace TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>;