using AutoMapper;
using MediatR;
using T_Shop.Application.Common.Exceptions;
using T_Shop.Application.Common.Helpers;
using T_Shop.Application.Common.ServiceInterface;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Product.ResponseModel;

namespace T_Shop.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponseModel>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductQueries _productQueries;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IGenericRepository<ProductImage> _productImageRepository;

        public UpdateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IProductQueries productQueries, ICloudinaryService cloudinaryService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productRepository = unitOfWork.GetBaseRepo<Product>();
            _productImageRepository = unitOfWork.GetBaseRepo<ProductImage>();
            _productQueries = productQueries;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<ProductResponseModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productQueries.GetByIdAsync(request.Id);
            if (product == null)
            {
                throw new NotFoundException("Product is not found");
            }
            //var color = await _colorRepository.GetById(request.ColorID);
            //if (color == null)
            //{
            //    throw new NotFoundException("Color is not found");
            //}
            //var model = await _modelRepository.GetById(request.ModelID);
            //if (model == null)
            //{
            //    throw new NotFoundException("Model is not found");
            //}
            //var type = await _typeRepository.GetById(request.TypeID);
            //if (type == null)
            //{
            //    throw new NotFoundException("Type is not found");
            //}

            var productUpdate = _mapper.Map<Product>(request);

            var imagesFromRequest = request.ImagesList;
            var imagesInProduct = product.ProductImages.ToList();
            List<ProductImage> imagesToDelete = new();
            //Delete images
            if (request.ImagesList.Count < product.ProductImages.Count)
            {
                imagesToDelete = imagesInProduct
                    .Where(imgInProduct =>
                          !imagesFromRequest
                            .Any(imgFromRequest => imgFromRequest.ImageID.Equals(imgInProduct.ImageID)))
                    .ToList();

                foreach (var imageToDelete in imagesToDelete)
                {
                    await _cloudinaryService.DeleteImageAsync(ImageHelpers.GetPublicIDFromImageUrl(imageToDelete.ImageUrl));
                }
            }

            //Upload images
            if (request.ImagesUpload.Count > 0)
            {
                int numOfImagesDeleted = imagesToDelete.Count;
                int numOfImagesToUpload = request.ImagesUpload.Count;
                int indexToUpload = 0;

                //Upload replace existed images
                if (numOfImagesDeleted > 0)
                {
                    for (int i = 0; i < numOfImagesDeleted; i++)
                    {
                        await _cloudinaryService.UpdateImageAsync(request.ImagesUpload[i], ImageHelpers.GetPublicIDFromImageUrl(imagesInProduct[i].ImageUrl));
                        indexToUpload++;
                        numOfImagesToUpload--;
                    }
                }
                //Upload new images
                if (numOfImagesToUpload > 0)
                {
                    List<ProductImage> productImagesUploaded = new List<ProductImage>();
                    while (numOfImagesToUpload > 0)
                    {

                        var imageAdded = await _cloudinaryService.AddImageAsync(request.ImagesUpload[indexToUpload]);
                        ProductImage productImageAdded = new ProductImage()
                        {
                            ProductID = productUpdate.Id,
                            ImageUrl = imageAdded.ImageUrl
                        };
                        productImagesUploaded.Add(productImageAdded);
                        numOfImagesToUpload--;
                    }
                    //Add to db
                    _productImageRepository.AddRange(productImagesUploaded);
                }
            }

            _productRepository.Update(productUpdate);
            await _unitOfWork.CompleteAsync();
            //productUpdate.Color = color;
            //productUpdate.Model = model;
            //productUpdate.Type = type;
            var result = _mapper.Map<ProductResponseModel>(productUpdate);
            return result;
        }
    }
}
