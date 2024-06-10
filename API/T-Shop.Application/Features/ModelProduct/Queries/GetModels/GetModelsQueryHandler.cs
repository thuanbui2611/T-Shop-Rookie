using AutoMapper;
using LazyCache;
using MediatR;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;

namespace T_Shop.Application.Features.ModelProduct.Queries.GetModelProducts;
public class GetModelsQueryHandler : IRequestHandler<GetModelsQuery, List<ModelProductResponseModel>>
{
    private readonly IMapper _mapper;
    private readonly IModelQueries _modelQueries;
    private readonly IAppCache _cache;
    private CacheKeyConstants _cacheKeyConstants;
    public GetModelsQueryHandler(IMapper mapper, IModelQueries modelQueries, IAppCache cache, CacheKeyConstants cacheKeyConstants)
    {
        _mapper = mapper;
        _modelQueries = modelQueries;
        _cache = cache;
        _cacheKeyConstants = cacheKeyConstants;
    }

    public async Task<List<ModelProductResponseModel>> Handle(GetModelsQuery request, CancellationToken cancellationToken)
    {
        var key = _cacheKeyConstants.ModelCacheKey;

        var models = await _cache.GetOrAddAsync(
            key,
            async () => await _modelQueries.GetModelsAsync(),
            TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours)
            );
        _cacheKeyConstants.AddKeyToList(key);

        var result = _mapper.Map<List<ModelProductResponseModel>>(models);
        return result;
    }
}
