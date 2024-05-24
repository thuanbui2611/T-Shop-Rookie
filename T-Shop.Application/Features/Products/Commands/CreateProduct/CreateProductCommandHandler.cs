using AutoMapper;
using LazyCache;
using MediatR;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.SharedServices.CloudinaryService;
using T_Shop.Shared.DTOs.Product.ResponseModel;

namespace T_Shop.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponseModel>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Domain.Entity.Color> _colorRepository;
        private readonly IModelQueries _modelQueries;
        private readonly IGenericRepository<TypeProduct> _typeRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IGenericRepository<ProductImage> _productImageRepository;

        private readonly IAppCache _cache;
        private CacheKeyConstants _cacheKeyConstants;

        public CreateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IModelQueries modelQueries, ICloudinaryService cloudinaryService, IAppCache cache, CacheKeyConstants cacheKeyConstants)
        {
            _mapper = mapper;
            _productRepository = unitOfWork.GetBaseRepo<Product>();
            _unitOfWork = unitOfWork;
            _colorRepository = _unitOfWork.GetBaseRepo<Domain.Entity.Color>();
            _typeRepository = _unitOfWork.GetBaseRepo<TypeProduct>();
            _modelQueries = modelQueries;
            _cloudinaryService = cloudinaryService;
            _productImageRepository = _unitOfWork.GetBaseRepo<ProductImage>();
            _cache = cache;
            _cacheKeyConstants = cacheKeyConstants;
        }

        public async Task<ProductResponseModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var color = await _colorRepository.GetById(request.ColorID);
            if (color == null)
            {
                throw new NotFoundException("Color is not found");
            }
            var model = await _modelQueries.GetModelByIdAsync(request.ModelID);
            if (model == null)
            {
                throw new NotFoundException("Model is not found");
            }
            var type = await _typeRepository.GetById(request.TypeID);
            if (type == null)
            {
                throw new NotFoundException("Type is not found");
            }

            var newProduct = _mapper.Map<Product>(request);
            _productRepository.Add(newProduct);
            //Upload images
            var images = await _cloudinaryService.AddImagesAsync(request.ImagesUpload);
            //Add images to table product image
            List<ProductImage> productImages = new List<ProductImage>();
            foreach (var image in images)
            {
                ProductImage productImage = new ProductImage()
                {
                    ProductID = newProduct.Id,
                    ImagePublicID = image.PublicID
                };
                productImages.Add(productImage);
            }
            //Set first image to main image
            productImages[0].IsMain = true;

            _productImageRepository.AddRange(productImages);

            await _unitOfWork.CompleteAsync();

            newProduct.Model = model;
            newProduct.Type = type;
            newProduct.Color = color;
            newProduct.ProductImages = productImages;

            UpdateExistedCache(newProduct);

            var result = _mapper.Map<ProductResponseModel>(newProduct);
            return result;
        }

        private async void UpdateExistedCache(Product newProduct)
        {
            var key = _cacheKeyConstants.ProductCacheKey;
            var cacheValues = await _cache.GetAsync<List<Product>>(key);
            if (cacheValues != null)
            {
                cacheValues.Add(newProduct);
                _cache.Add(key, cacheValues);
            }
        }
    }
}
