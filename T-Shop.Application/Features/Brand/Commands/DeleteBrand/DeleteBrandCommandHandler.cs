using LazyCache;
using MediatR;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.Features.Brand.Command.DeleteBrand;
public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, bool>
{
    private readonly IGenericRepository<Domain.Entity.Brand> _brandRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppCache _cache;
    private CacheKeyConstants _cacheKeyConstants;
    public DeleteBrandCommandHandler(IUnitOfWork unitOfWork, IAppCache cache, CacheKeyConstants cacheKeyConstants)
    {
        _brandRepository = unitOfWork.GetBaseRepo<Domain.Entity.Brand>();
        _unitOfWork = unitOfWork;
        _cache = cache;
        _cacheKeyConstants = cacheKeyConstants;
    }

    public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetById(request.ID);
        if (brand == null)
        {
            throw new NotFoundException("Brand not found");
        }
        _brandRepository.Delete(request.ID);
        await _unitOfWork.CompleteAsync();

        UpdateExistedCache(brand);

        return true;
    }

    private async void UpdateExistedCache(Domain.Entity.Brand deletedBrand)
    {
        var key = _cacheKeyConstants.BrandCacheKey;
        var cacheValues = await _cache.GetAsync<List<Domain.Entity.Brand>>(key);
        if (cacheValues != null)
        {
            cacheValues.RemoveAll(t => t.Id.Equals(deletedBrand.Id));
            _cache.Add(key, cacheValues);
        }
    }
}
