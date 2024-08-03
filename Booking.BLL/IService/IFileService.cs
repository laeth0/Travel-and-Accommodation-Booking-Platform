

using Microsoft.AspNetCore.Http;

namespace Booking.BLL.IService
{
    public interface IFileService
    {
        public Task<string?> UploadFile(IFormFile file, string FolderName = "images");
        public void DeleteFile(string FileName, string FolderName = "images");

    }
}
