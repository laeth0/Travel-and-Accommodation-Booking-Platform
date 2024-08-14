﻿using Booking.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Booking.Infrastrature.Services.Files;
public class FileService : IFileService
{
    public async Task<string?> UploadFileAsync(IFormFile file, string FolderName = "images")// => IFormFile is the type of the file that we want to upload
    {

        if (file is null)
            return null;

        // 1 ) Folder Path
        string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName);


        // 2 ) File Name
        string FileName = $"{Guid.NewGuid()}{file.FileName}";


        // 3 ) File Path
        string FilePath = Path.Combine(FolderPath, FileName);


        // 4 ) save file as stream   => stream is a sequence of bytes mean upload data per time
        using var FileStream = new FileStream(FilePath, FileMode.Create);


        await file.CopyToAsync(FileStream);
        return FileName;
    }



    public void DeleteFile(string FileName, string FolderName = "images")
    {
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName, FileName);
        if (File.Exists(imagePath))
            File.Delete(imagePath);
    }
}
