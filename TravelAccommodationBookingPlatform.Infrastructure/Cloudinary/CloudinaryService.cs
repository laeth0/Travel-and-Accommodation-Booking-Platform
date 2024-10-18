using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using TravelAccommodationBookingPlatform.Application.Constants;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;

namespace TravelAccommodationBookingPlatform.Infrastructure.Cloudinary;
public class CloudinaryService : ICloudinaryService
{
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;
    public CloudinaryService(IOptionsMonitor<CloudinaryConfig> options)
    {
        var account = new CloudinaryDotNet.Account
            (
                options.CurrentValue.CloudName,
                options.CurrentValue.ApiKey,
                options.CurrentValue.ApiSecret
            );

        _cloudinary = new CloudinaryDotNet.Cloudinary(account);
    }



    public async Task<Result<string>> UploadeAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        if (IsNullOrWrongSize(file))
            Result.Failure<string>(ApplicationErrors.File.UploadFailed());

        using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams
        {
            DisplayName = file.FileName,
            Folder = "Booking",
            File = new CloudinaryDotNet.FileDescription(file.FileName, stream),
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken);

        return uploadResult.Error is null
              ? Result.Success(uploadResult.SecureUrl.AbsoluteUri)
              : Result.Failure<string>(ApplicationErrors.File.UploadFailed(uploadResult.Error.Message));

    }


    public async Task<Result> DeleteAsync(string imageUrl)
    {
        var publicId = GetPublicIdFromUrl(imageUrl);

        var deletionParams = new DeletionParams(publicId);

        var result = await _cloudinary.DestroyAsync(deletionParams);

        Result.Failure(ApplicationErrors.File.DeletionFailed(result.Error.Message));

        return result.Error is null
            ? Result.Success()
            : Result.Failure(ApplicationErrors.File.DeletionFailed(result.Error.Message));
    }


    private static string GetPublicIdFromUrl(string url)
    {
        var uri = new Uri(url);
        return Path.GetFileNameWithoutExtension(uri.LocalPath);
    }


    private static bool IsNullOrWrongSize(IFormFile file)
    {
        return file is null || file.Length <= 0;
    }
}