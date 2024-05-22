using AutoMapper;
using LazyCache;
using MediatR;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.Application.Features.Type.Queries.GetTypes;
public class GetTypesQueryHandler : IRequestHandler<GetTypesQuery, List<TypeResponseModel>>
{
    private readonly IAppCache _cache;
    private CacheKeyConstants _cacheKeyConstants;
    private readonly IMapper _mapper;
    private readonly ITypeQueries _typeQueries;

    public GetTypesQueryHandler(IMapper mapper, ITypeQueries typeQueries, IAppCache cache, CacheKeyConstants cacheKeyConstants)
    {
        _mapper = mapper;
        _typeQueries = typeQueries;
        _cache = cache;
        _cacheKeyConstants = cacheKeyConstants;
    }

    public async Task<List<TypeResponseModel>> Handle(GetTypesQuery request, CancellationToken cancellationToken)
    {
        var key = _cacheKeyConstants.TypeCacheKey;

        var types = await _cache.GetOrAddAsync(
            key,
            async () => await _typeQueries.GetTypesAsync(),
            TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours)
            );

        var result = _mapper.Map<List<TypeResponseModel>>(types);
        return result;
    }
}