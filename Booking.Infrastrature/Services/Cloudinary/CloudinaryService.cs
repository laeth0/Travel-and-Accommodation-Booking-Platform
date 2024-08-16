


using Booking.Domain.Interfaces.Services;
using Booking.Domain.Models;
using Booking.PL.CustomExceptions;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Booking.Infrastrature.Services;
public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;
    public CloudinaryService(IOptions<CloudinaryConfig> options)
    {
        var account = new Account
            (
                options.Value.CloudName,
                options.Value.ApiKey,
                options.Value.ApiSecret
            );

        _cloudinary = new Cloudinary(account);
    }


    public async Task<CloudinaryImage> UploadImageAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(file);

        if (file.Length <= 0)
            throw new ArgumentException("File is empty", nameof(file));


        using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams
        {
            DisplayName = file.FileName,
            Folder = "Booking",
            File = new FileDescription(file.FileName, stream),
            //Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face") 
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken);

        if (uploadResult.Error is { }) // this is a pattern matching mean if uploadResult.Error is not null
            throw new BadRequestException(uploadResult.Error.Message);


        var Image = new CloudinaryImage
        {
            PublicId = uploadResult.PublicId,
            Url = uploadResult.SecureUrl.AbsoluteUri
        };

        return Image;  //return uploadResult;

    }




    public async Task DeleteImageAsync(string publicId)
    {
        if (string.IsNullOrEmpty(publicId))
            throw new ArgumentNullException(nameof(publicId));

        var deleteParams = new DeletionParams(publicId);

        var result = await _cloudinary.DestroyAsync(deleteParams);

        if (result.Error is { })
            throw new BadRequestException(result.Error.Message);

    }





    public async Task<CloudinaryImage> ReplaceImageAsync(string publicId, IFormFile newFile, CancellationToken cancellationToken = default)
    {
        await DeleteImageAsync(publicId);

        var CloudinaryImage = await UploadImageAsync(newFile, cancellationToken);

        return CloudinaryImage;
    }
}


