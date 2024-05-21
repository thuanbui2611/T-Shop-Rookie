using AutoMapper;
using Diacritics.Extensions;
using LazyCache;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using T_Shop.Application.Common.Constants;
using T_Shop.Application.Common.Helpers;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.ModelProduct.QueryModel;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;
using T_Shop.Shared.DTOs.Pagination;

namespace T_Shop.Application.Features.ModelProduct.Queries.GetModelProducts;
public class GetModelsProductQueryHandler : IRequestHandler<GetModelsProductQuery, (List<ModelProductResponseModel>, PaginationMetaData)>
{
    private readonly IMapper _mapper;
    private readonly IModelQueries _modelQueries;
    private readonly IAppCache _cache;
    private CacheKeyConstants _cacheKeyConstants;
    public GetModelsProductQueryHandler(IMapper mapper, IModelQueries modelQueries, IAppCache cache, CacheKeyConstants cacheKeyConstants)
    {
        _mapper = mapper;
        _modelQueries = modelQueries;
        _cache = cache;
        _cacheKeyConstants = cacheKeyConstants;
    }

    public async Task<(List<ModelProductResponseModel>, PaginationMetaData)> Handle(GetModelsProductQuery request, CancellationToken cancellationToken)
    {
        var key = $"{_cacheKeyConstants.ModelCacheKey}-All";

        var models = await _cache.GetOrAddAsync(
            key,
            async () => await _modelQueries.GetModelsAsync(),
            TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours)
            );
        _cacheKeyConstants.AddKeyToList(key);

        models = HandleModelQuery(request.ModelQuery, models);

        var (modelsPaginated, pagination) = PaginationHelpers.GetPaginationModel(models, request.Pagination);

        var result = _mapper.Map<List<ModelProductResponseModel>>(modelsPaginated);
        return (result, pagination);
    }

    private List<Model> HandleModelQuery(ModelQuery modelQuery, List<Model> models)
    {
        //Search
        if (!modelQuery.Search.IsNullOrEmpty())
        {
            string trimmedSearch = modelQuery.Search.Trim().ToLower().RemoveDiacritics();
            string[] searchTerms = trimmedSearch.Split(' ');
            models = models.Where(x =>
                searchTerms.Any(s =>
                    x.Name.Trim().ToLower().RemoveDiacritics().Contains(s) ||
                    x.Brand.Name.Trim().ToLower().RemoveDiacritics().Contains(s) ||
                    x.Year.ToString().Contains(s)
                ))
                .ToList();
        }
        return models;
    }
}
