using AutoMapper;
using Diacritics.Extensions;
using LazyCache;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using T_Shop.Application.Common.Constants;
using T_Shop.Application.Common.Helpers;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Brand.QueryModel;
using T_Shop.Shared.DTOs.Brand.ResponseModel;
using T_Shop.Shared.DTOs.Pagination;

namespace T_Shop.Application.Features.Brand.Queries.GetBrands;
public class GetBrandsPaginationQueryHandler : IRequestHandler<GetBrandsPaginationQuery, (List<BrandResponseModel>, PaginationMetaData)>
{
    private readonly IAppCache _cache;
    private CacheKeyConstants _cacheKeyConstants;
    private readonly IMapper _mapper;
    private readonly IBrandQueries _brandQueries;

    public GetBrandsPaginationQueryHandler(IMapper mapper, IBrandQueries brandQueries, IAppCache cache, CacheKeyConstants cacheKeyConstants)
    {
        _mapper = mapper;
        _brandQueries = brandQueries;
        _cache = cache;
        _cacheKeyConstants = cacheKeyConstants;
    }

    public async Task<(List<BrandResponseModel>, PaginationMetaData)> Handle(GetBrandsPaginationQuery request, CancellationToken cancellationToken)
    {
        var key = _cacheKeyConstants.BrandCacheKey;
        var brands = await _cache.GetOrAddAsync(
        key,
            async () => await _brandQueries.GetBrandsAsync(),
            TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours)
            );
        _cacheKeyConstants.AddKeyToList(key);

        brands = HandleBrandQuery(request.BrandQuery, brands);
        var (brandsPaginated, pagination) = PaginationHelpers.GetPaginationModel(brands, request.Pagination);
        var result = _mapper.Map<List<BrandResponseModel>>(brandsPaginated);
        return (result, pagination);
    }

    private List<Domain.Entity.Brand> HandleBrandQuery(BrandQuery? brandQuery, List<Domain.Entity.Brand> brands)
    {
        //Search
        if (brandQuery != null && !brandQuery.Search.IsNullOrEmpty())
        {
            string trimmedSearch = brandQuery.Search!.Trim().ToLower().RemoveDiacritics();
            string[] searchTerms = trimmedSearch.Split(' ');
            brands = brands.Where(x =>
                searchTerms.Any(s =>
                    x.Name.Trim().ToLower().RemoveDiacritics().Contains(s)
                ))
                .ToList();
        }
        return brands;
    }
}
