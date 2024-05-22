using LazyCache;
using MediatR;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.Features.ModelProduct.Commands.DeleteModelProduct;
public class DeleteModelProductCommandHandler : IRequestHandler<DeleteModelProductCommand, bool>
{
    private readonly IGenericRepository<Model> _modelRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppCache _cache;
    private CacheKeyConstants _cacheKeyConstants;
    public DeleteModelProductCommandHandler(IUnitOfWork unitOfWork, IAppCache cache, CacheKeyConstants cacheKeyConstants)
    {
        _unitOfWork = unitOfWork;
        _modelRepository = _unitOfWork.GetBaseRepo<Model>();
        _cache = cache;
        _cacheKeyConstants = cacheKeyConstants;
    }

    public async Task<bool> Handle(DeleteModelProductCommand request, CancellationToken cancellationToken)
    {
        var model = await _modelRepository.GetById(request.ID);
        if (model == null)
        {
            throw new NotFoundException("Model not found");
        }
        _modelRepository.Delete(request.ID);
        await _unitOfWork.CompleteAsync();

        UpdateExistedCache(model);

        return true;
    }

    private async void UpdateExistedCache(Model deletedModel)
    {
        var key = _cacheKeyConstants.ModelCacheKey;
        var cacheValues = await _cache.GetAsync<List<TypeProduct>>(key);
        if (cacheValues != null)
        {
            cacheValues.RemoveAll(t => t.Id.Equals(deletedModel.Id));
            _cache.Add(key, cacheValues);
        }
    }
}
