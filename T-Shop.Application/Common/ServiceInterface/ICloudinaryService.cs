using Microsoft.AspNetCore.Http;
using T_Shop.Domain.Entity.ServiceEntity.Cloudinary;

namespace T_Shop.Application.Common.ServiceInterface;
public interface ICloudinaryService
{
    Task<CloudinaryResult> AddPhotoAsync(IFormFile? file);
    Task<string> DeletePhotoAsync(string publicID);
}
