using AutoMapper;
using Diacritics.Extensions;
using LazyCache;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using T_Shop.Application.Common.Constants;
using T_Shop.Application.Common.Helpers;
using T_Shop.Application.Features.Type.Queries.GetTypesPagination;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Pagination;
using T_Shop.Shared.DTOs.Type.QueryModel;
using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.Application.Features.Type.Queries.GetTypes;
public class GetTypesPaginationQueryHandler : IRequestHandler<GetTypesPaginationQuery, (List<TypeResponseModel>, PaginationMetaData)>
{
    private readonly IAppCache _cache;
    private CacheKeyConstants _cacheKeyConstants;
    private readonly IMapper _mapper;
    private readonly ITypeQueries _typeQueries;

    public GetTypesPaginationQueryHandler(IMapper mapper, ITypeQueries typeQueries, IAppCache cache, CacheKeyConstants cacheKeyConstants)
    {
        _mapper = mapper;
        _typeQueries = typeQueries;
        _cache = cache;
        _cacheKeyConstants = cacheKeyConstants;
    }

    public async Task<(List<TypeResponseModel>, PaginationMetaData)> Handle(GetTypesPaginationQuery request, CancellationToken cancellationToken)
    {
        var key = _cacheKeyConstants.TypeCacheKey;

        var types = await _cache.GetOrAddAsync(
            key,
            async () => await _typeQueries.GetTypesAsync(),
            TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours)
            );
        _cacheKeyConstants.AddKeyToList(key);
        types = HandleTypeQuery(request.TypeQuery, types);

        var (typesPaginated, pagination) = PaginationHelpers.GetPaginationModel(types, request.Pagination);

        var result = _mapper.Map<List<TypeResponseModel>>(typesPaginated);
        return (result, pagination);
    }

    private List<TypeProduct> HandleTypeQuery(TypeQuery typeQuery, List<TypeProduct> types)
    {
        //Search
        if (!typeQuery.Search.IsNullOrEmpty())
        {
            string trimmedSearch = typeQuery.Search.Trim().ToLower().RemoveDiacritics();
            string[] searchTerms = trimmedSearch.Split(' ');
            types = types.Where(x =>
                searchTerms.Any(s =>
                    x.Name.Trim().ToLower().RemoveDiacritics().Contains(s)
                ))
                .ToList();
        }
        return types;
    }
}