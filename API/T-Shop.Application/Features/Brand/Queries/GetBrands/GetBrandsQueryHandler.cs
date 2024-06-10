using AutoMapper;
using LazyCache;
using MediatR;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Brand.ResponseModel;

namespace T_Shop.Application.Features.Brand.Queries.GetBrands;
public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, List<BrandResponseModel>>
{
    private readonly IAppCache _cache;
    private CacheKeyConstants _cacheKeyConstants;
    private readonly IMapper _mapper;
    private readonly IBrandQueries _brandQueries;

    public GetBrandsQueryHandler(IMapper mapper, IBrandQueries brandQueries, IAppCache cache, CacheKeyConstants cacheKeyConstants)
    {
        _mapper = mapper;
        _brandQueries = brandQueries;
        _cache = cache;
        _cacheKeyConstants = cacheKeyConstants;
    }

    public async Task<List<BrandResponseModel>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        var key = _cacheKeyConstants.BrandCacheKey;
        var brands = await _cache.GetOrAddAsync(
        key,
            async () => await _brandQueries.GetBrandsAsync(),
            TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours)
            );
        _cacheKeyConstants.AddKeyToList(key);
        var result = _mapper.Map<List<BrandResponseModel>>(brands);
        return result;
    }
}
