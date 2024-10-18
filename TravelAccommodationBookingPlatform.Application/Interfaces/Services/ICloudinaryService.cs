using Microsoft.AspNetCore.Http;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;

namespace TravelAccommodationBookingPlatform.Application.Interfaces;
public interface ICloudinaryService : ITransientService
{

    Task<Result<string>> UploadeAsync(IFormFile file, CancellationToken cancellationToken = default);

    Task<Result> DeleteAsync(string imageUrl);
}
