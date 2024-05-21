using AutoMapper;
using LazyCache;
using MediatR;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Color.ResponseModel;

namespace T_Shop.Application.Features.Color.Queries.GetColors;
public class GetColorsQueryHandler : IRequestHandler<GetColorsQuery, List<ColorResponseModel>>
{
    private readonly IAppCache _cache;
    private CacheKeyConstants _cacheKeyConstants;
    private readonly IMapper _mapper;
    private readonly IColorQueries _colorQueries;

    public GetColorsQueryHandler(IMapper mapper, IColorQueries colorQueries, IAppCache cache, CacheKeyConstants cacheKeyConstants)
    {
        _mapper = mapper;
        _colorQueries = colorQueries;
        _cache = cache;
        _cacheKeyConstants = cacheKeyConstants;
    }

    public async Task<List<ColorResponseModel>> Handle(GetColorsQuery request, CancellationToken cancellationToken)
    {
        var key = $"{_cacheKeyConstants.ColorCacheKey}-All";

        var colors = await _cache.GetOrAddAsync(
            key,
            async () => await _colorQueries.GetColorsAsync(),
            TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours)
            );
        _cacheKeyConstants.AddKeyToList(key);
        var result = _mapper.Map<List<ColorResponseModel>>(colors);
        return result;
    }
}
