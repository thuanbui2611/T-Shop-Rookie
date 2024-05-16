using Microsoft.AspNetCore.Http;
using T_Shop.Domain.Entity;
using T_Shop.Shared.DTOs.Image.ResponseModel;

namespace T_Shop.Application.Common.ServiceInterface;
public interface IImageService
{
    Task<ImageResponseModel> AddImage(IFormFile file);
    Task<List<ImageResponseModel>> AddImagesProduct(IFormFileCollection files, Guid productID);
    Task<bool> UpdateImage(IFormFile file, string publicID);
    Task<string> DeleteImage(Image image);
}
