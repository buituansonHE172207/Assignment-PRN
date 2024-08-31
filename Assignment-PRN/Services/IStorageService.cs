using Microsoft.AspNetCore.Http;

namespace Assignment_PRN.Services;

public interface IStorageService
{
    Task<string> UploadImage(IFormFile file);
    Task<string> UploadVideo(IFormFile file);
    Task<string> GetImageUrl(string fileId);
    Task<string> GetVideoUrl(string fileId);
    
    Task DeleteFile(string fileId);
}