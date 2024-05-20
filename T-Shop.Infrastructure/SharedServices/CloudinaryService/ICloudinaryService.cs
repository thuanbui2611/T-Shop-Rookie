using Microsoft.AspNetCore.Http;

namespace T_Shop.Infrastructure.SharedServices.CloudinaryService;
public interface ICloudinaryService
{
    Task<CloudinaryResult> AddImageAsync(IFormFile? file);
    Task<List<CloudinaryResult>> AddImagesAsync(IFormFileCollection files);
    Task<bool> UpdateImageAsync(IFormFile file, string publicID);
    Task<string> DeleteImageAsync(string publicID);
}
