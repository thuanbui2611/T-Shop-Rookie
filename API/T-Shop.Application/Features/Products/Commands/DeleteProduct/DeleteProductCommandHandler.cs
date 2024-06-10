using LazyCache;
using MediatR;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.SharedServices.CloudinaryService;

namespace T_Shop.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductImage> _imageProductRepository;
        private readonly IProductQueries _productQueries;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IAppCache _cache;
        private CacheKeyConstants _cacheKeyConstants;
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IProductQueries productQueries, ICloudinaryService cloudinaryService, IAppCache cache, CacheKeyConstants cacheKeyConstants)
        {
            _productRepository = unitOfWork.GetBaseRepo<Product>();
            _imageProductRepository = unitOfWork.GetBaseRepo<ProductImage>();
            _unitOfWork = unitOfWork;
            _productQueries = productQueries;
            _cloudinaryService = cloudinaryService;
            _cache = cache;
            _cacheKeyConstants = cacheKeyConstants;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productQueries.GetProductByIdAsync(request.Id);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }

            //Delete product image
            foreach (var image in product.ProductImages)
            {
                await _cloudinaryService.DeleteImageAsync(image.ImagePublicID);
            }
            _imageProductRepository.DeleteRange(product.ProductImages);
            _productRepository.Delete(request.Id);
            await _unitOfWork.CompleteAsync();

            UpdateExistedCache(product);

            return true;
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
