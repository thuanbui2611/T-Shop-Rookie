using LazyCache;
using MediatR;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.Features.Products.Commands.UpdateProductStatus;
public class LockOrUnlockProductCommandHandler : IRequestHandler<LockOrUnlockProductCommand, bool>
{
    private readonly IGenericRepository<Domain.Entity.Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IAppCache _cache;
    private CacheKeyConstants _cacheKeyConstants;

    public LockOrUnlockProductCommandHandler(IUnitOfWork unitOfWork, IAppCache cache, CacheKeyConstants cacheKeyConstants)
    {
        _unitOfWork = unitOfWork;
        _productRepository = unitOfWork.GetBaseRepo<Domain.Entity.Product>();
        _cache = cache;
        _cacheKeyConstants = cacheKeyConstants;
    }

    public async Task<bool> Handle(LockOrUnlockProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.ProductID);
        if (product == null) throw new NotFoundException("Product not found!");

        product.IsOnStock = !product.IsOnStock;

        _productRepository.Update(product);
        await _unitOfWork.CompleteAsync();

        UpdateExistedCache(product.Id);

        return true;
    }

    private async void UpdateExistedCache(Guid productId)
    {
        var key = _cacheKeyConstants.ProductCacheKey;
        var cacheValues = await _cache.GetAsync<List<Domain.Entity.Product>>(key);
        if (cacheValues != null)
        {
            var productInCache = cacheValues.Find(t => t.Id.Equals(productId));
            if (productInCache == null) return;

            cacheValues.RemoveAll(t => t.Id.Equals(productInCache?.Id));

            productInCache.IsOnStock = !productInCache.IsOnStock;
            cacheValues.Add(productInCache);
            _cache.Add(key, cacheValues);
        }
    }
}
