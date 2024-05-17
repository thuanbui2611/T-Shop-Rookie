using Microsoft.AspNetCore.Http;
using T_Shop.Domain.Entity.ServiceEntity.Cloudinary;

namespace T_Shop.Application.Common.ServiceInterface;
public interface ICloudinaryService
{
    Task<CloudinaryResult> AddImageAsync(IFormFile? file);
    Task<List<CloudinaryResult>> AddImagesAsync(IFormFileCollection files);
    Task<bool> UpdateImageAsync(IFormFile file, string publicID);
    Task<string> DeleteImageAsync(string publicID);
}
