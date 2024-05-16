using Microsoft.AspNetCore.Http;
using T_Shop.Application.Common.ServiceInterface;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Image.ResponseModel;

namespace T_Shop.Infrastructure.SharedServices.ImageService;
public class ImageService : IImageService
{
    private readonly IGenericRepository<Image> _imageRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryService _cloudinaryService;

    public ImageService(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
    {
        _unitOfWork = unitOfWork;
        _imageRepository = unitOfWork.GetBaseRepo<Image>();
        _cloudinaryService = cloudinaryService;
    }

    public async Task<ImageResponseModel> AddImage(IFormFile file)
    {
        if (file is null) return null;

        var uploadImageResult = await _cloudinaryService.AddPhotoAsync(file);
        Image image = new Image()
        {
            Id = Guid.NewGuid(),
            PublicID = uploadImageResult.PublicID,
            ImageURL = uploadImageResult.ImageUrl
        };
        _imageRepository.Add(image);
        await _unitOfWork.CompleteAsync();

        var result = new ImageResponseModel()
        {
            ID = image.Id,
            PublicID = uploadImageResult.PublicID,
            ImageUrl = uploadImageResult.ImageUrl
        };
        return result;
    }

    public async Task<string> DeleteImage(Image image)
    {
        var deleteImageFromCloudinary = await _cloudinaryService.DeletePhotoAsync(image.PublicID);
        //if(deleteImageFromCloudinary == null)
        _imageRepository.Delete(image.Id);
        await _unitOfWork.CompleteAsync();
        return deleteImageFromCloudinary;
    }
}
