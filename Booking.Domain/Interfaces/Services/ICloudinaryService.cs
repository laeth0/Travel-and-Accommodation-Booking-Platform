


using Booking.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Booking.Domain.Interfaces.Services;
public interface ICloudinaryService
{
    Task<CloudinaryImage> UploadImageAsync(IFormFile file, CancellationToken cancellationToken = default);

    Task DeleteImageAsync(string publicId);

    Task<CloudinaryImage> ReplaceImageAsync(string publicId, IFormFile newFile, CancellationToken cancellationToken = default);

}
