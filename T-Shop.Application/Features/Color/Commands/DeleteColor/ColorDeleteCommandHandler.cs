using LazyCache;
using MediatR;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.Features.Color.Commands.DeleteColor;
public class ColorDeleteCommandHandler : IRequestHandler<ColorDeleteCommand, bool>
{
    private readonly IGenericRepository<Domain.Entity.Color> _colorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppCache _cache;
    private CacheKeyConstants _cacheKeyConstants;
    public ColorDeleteCommandHandler(IUnitOfWork unitOfWork, IAppCache cache, CacheKeyConstants cacheKeyConstants)
    {
        _colorRepository = unitOfWork.GetBaseRepo<Domain.Entity.Color>();
        _unitOfWork = unitOfWork;
        _cache = cache;
        _cacheKeyConstants = cacheKeyConstants;
    }

    public async Task<bool> Handle(ColorDeleteCommand request, CancellationToken cancellationToken)
    {
        var color = await _colorRepository.GetById(request.ID);
        if (color == null)
        {
            throw new NotFoundException("Color not found");
        }
        _colorRepository.Delete(request.ID);
        await _unitOfWork.CompleteAsync();

        UpdateExistedCache(color);

        return true;
    }

    private async void UpdateExistedCache(Domain.Entity.Color deletedColor)
    {
        var key = _cacheKeyConstants.ColorCacheKey;
        var cacheValues = await _cache.GetAsync<List<Domain.Entity.Color>>(key);
        if (cacheValues != null)
        {
            cacheValues.RemoveAll(t => t.Id.Equals(deletedColor.Id));
            _cache.Add(key, cacheValues);
        }
    }
}
