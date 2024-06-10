using LazyCache;
using MediatR;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.Features.Type.Commands.DeleteType;
public class DeleteTypeCommandHandler : IRequestHandler<DeleteTypeCommand, bool>
{
    private readonly IGenericRepository<TypeProduct> _typeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppCache _cache;
    private CacheKeyConstants _cacheKeyConstants;
    public DeleteTypeCommandHandler(IUnitOfWork unitOfWork, IAppCache cache, CacheKeyConstants cacheKeyConstants)
    {
        _unitOfWork = unitOfWork;
        _typeRepository = _unitOfWork.GetBaseRepo<TypeProduct>();
        _cache = cache;
        _cacheKeyConstants = cacheKeyConstants;
    }

    public async Task<bool> Handle(DeleteTypeCommand request, CancellationToken cancellationToken)
    {
        var type = await _typeRepository.GetById(request.Id);
        if (type == null)
        {
            throw new NotFoundException("Type not found");
        }
        _typeRepository.Delete(request.Id);
        await _unitOfWork.CompleteAsync();

        UpdateExistedCache(type);

        return true;
    }

    private async void UpdateExistedCache(TypeProduct deletedType)
    {
        var key = _cacheKeyConstants.TypeCacheKey;
        var cacheValues = await _cache.GetAsync<List<TypeProduct>>(key);
        if (cacheValues != null)
        {
            cacheValues.RemoveAll(t => t.Id.Equals(deletedType.Id));
            _cache.Add(key, cacheValues);
        }
    }
}
