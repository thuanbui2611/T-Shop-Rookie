using Microsoft.AspNetCore.Http;
using T_Shop.Domain.Entity;
using T_Shop.Shared.DTOs.Image.ResponseModel;

namespace T_Shop.Application.Common.ServiceInterface;
public interface IImageService
{
    Task<ImageResponseModel> AddImage(IFormFile file);
    Task<string> DeleteImage(Image image);
}
