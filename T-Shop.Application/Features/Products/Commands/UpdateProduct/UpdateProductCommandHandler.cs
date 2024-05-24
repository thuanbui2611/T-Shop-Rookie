using AutoMapper;
using LazyCache;
using MediatR;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.SharedServices.CloudinaryService;
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
        private readonly IAppCache _cache;
        private CacheKeyConstants _cacheKeyConstants;

        public UpdateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IProductQueries productQueries, ICloudinaryService cloudinaryService, CacheKeyConstants cacheKeyConstants, IAppCache cache)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productRepository = unitOfWork.GetBaseRepo<Product>();
            _productImageRepository = unitOfWork.GetBaseRepo<ProductImage>();
            _productQueries = productQueries;
            _cloudinaryService = cloudinaryService;
            _cacheKeyConstants = cacheKeyConstants;
            _cache = cache;
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

            List<ProductImage> imagesToDelete = imagesInProduct
                    .Where(imgInProduct =>
                          !imagesFromRequest
                            .Any(imgFromRequest => imgFromRequest.imageID.Equals(imgInProduct.ImageID)))
                    .ToList();

            //Handle upload images
            if (request.ImagesUpload.Count > 0)
            {
                //int indexToUpload = 0;
                //Upload replace existed images
                //if (imagesToDelete.Count > 0)
                //{
                //    for (int i = 0; i < imagesToDelete.Count; i++)
                //    {
                //        if (numOfImagesToUpload == 0) break;

                //        await _cloudinaryService.UpdateImageAsync(request.ImagesUpload[i], imagesToDelete[i].ImagePublicID);

                //        imagesToDelete.RemoveAt(i);
                //        indexToUpload++;
                //        numOfImagesToUpload--;
                //    }
                //}
                //Upload new images
                if (request.ImagesUpload.Count > 0)
                {
                    List<ProductImage> productImagesUploaded = new List<ProductImage>();
                    for (int i = 0; i < request.ImagesUpload.Count; i++)
                    {
                        var imageAdded = await _cloudinaryService.AddImageAsync(request.ImagesUpload[i]);
                        ProductImage productImageAdded = new ProductImage()
                        {
                            ProductID = productUpdate.Id,
                            ImagePublicID = imageAdded.PublicID
                        };
                        productImagesUploaded.Add(productImageAdded);
                    }
                    //Add to db
                    _productImageRepository.AddRange(productImagesUploaded);
                }
            }
            //Check if imageDeleted is deleted all
            if (imagesToDelete.Count > 0)
            {
                for (int i = 0; i < imagesToDelete.Count; i++)
                {
                    await _cloudinaryService.DeleteImageAsync(imagesToDelete[i].ImagePublicID);
                }
                _productImageRepository.DeleteRange(imagesToDelete);
            }

            productUpdate.CreatedAt = product.CreatedAt;
            productUpdate.LastUpdated = DateTime.UtcNow;
            _productRepository.Update(productUpdate);
            await _unitOfWork.CompleteAsync();
            //productUpdate.Color = color;
            //productUpdate.Model = model;
            //productUpdate.Type = type;

            UpdateExistedCache(productUpdate);

            var result = _mapper.Map<ProductResponseModel>(productUpdate);
            return result;
        }

        private async void UpdateExistedCache(Product deletedProduct)
        {
            var key = _cacheKeyConstants.ProductCacheKey;
            var cacheValues = await _cache.GetAsync<List<Product>>(key);
            if (cacheValues != null)
            {
                cacheValues.RemoveAll(t => t.Id.Equals(deletedProduct.Id));
                _cache.Add(key, cacheValues);
            }
        }
    }
}
