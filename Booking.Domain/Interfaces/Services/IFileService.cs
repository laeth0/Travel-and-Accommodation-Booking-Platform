using Microsoft.AspNetCore.Http;

namespace Booking.Domain.Interfaces.Services;
public interface IFileService
{
    public Task<string?> UploadFileAsync(IFormFile file, string FolderName = "images");
    public void DeleteFile(string FileName, string FolderName = "images");
}
