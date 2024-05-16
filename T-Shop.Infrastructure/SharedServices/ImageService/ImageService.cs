using AutoMapper;
using Microsoft.AspNetCore.Http;
using T_Shop.Application.Common.ServiceInterface;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Image.ResponseModel;

namespace T_Shop.Infrastructure.SharedServices.ImageService;
public class ImageService : IImageService
{
    private readonly IGenericRepository<Image> _imageRepository;
    private readonly IGenericRepository<ProductImage> _imageProductRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IMapper _mapper;

    public ImageService(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _imageRepository = unitOfWork.GetBaseRepo<Image>();
        _imageProductRepository = unitOfWork.GetBaseRepo<ProductImage>();
        _cloudinaryService = cloudinaryService;
        _mapper = mapper;
    }

    public async Task<ImageResponseModel> AddImage(IFormFile file)
    {
        if (file is null) return null;

        var uploadImageResult = await _cloudinaryService.AddImageAsync(file);
        Image image = new Image()
        {
            Id = Guid.NewGuid(),
            PublicID = uploadImageResult.PublicID
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

    public async Task<bool> UpdateImage(IFormFile file, string publicID)
    {
        if (file is null || publicID is null) return false;
        await _cloudinaryService.UpdateImageAsync(file, publicID);
        return true;
    }

    public async Task<List<ImageResponseModel>> AddImagesProduct(IFormFileCollection files, Guid productID)
    {
        if (files is null) return null;

        List<Image> imagesUploaded = new();
        List<ProductImage> productImages = new();
        foreach (var file in files)
        {
            var uploadImageResult = await _cloudinaryService.AddImageAsync(file);

            //Add to images
            Image imageUploaded = new()
            {
                Id = Guid.NewGuid(),
                PublicID = uploadImageResult.PublicID,
                ImageURL = uploadImageResult.ImageUrl

            };
            imagesUploaded.Add(imageUploaded);

            //Add to product images
            ProductImage productImage = new()
            {
                ProductID = productID,
                ImageID = imageUploaded.Id,
            };
            productImages.Add(productImage);
        }

        _imageRepository.AddRange(imagesUploaded);
        _imageProductRepository.AddRange(productImages);

        await _unitOfWork.CompleteAsync();

        var result = _mapper.Map<List<ImageResponseModel>>(imagesUploaded);
        return result;
    }

    public async Task<string> DeleteImage(Image image)
    {
        var deleteImageFromCloudinary = await _cloudinaryService.DeleteImageAsync(image.PublicID);
        //if(deleteImageFromCloudinary == null)
        _imageRepository.Delete(image.Id);
        await _unitOfWork.CompleteAsync();
        return deleteImageFromCloudinary;
    }
}
