using AutoMapper;
using Diacritics.Extensions;
using LazyCache;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using T_Shop.Application.Common.Constants;
using T_Shop.Application.Common.Helpers;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Pagination;
using T_Shop.Shared.DTOs.Product.QueryModel;
using T_Shop.Shared.DTOs.Product.ResponseModel;

namespace T_Shop.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, (List<ProductResponseModel>, PaginationMetaData)>
    {
        private readonly IAppCache _cache;
        private CacheKeyConstants _cacheKeyConstants;
        private readonly IMapper _mapper;
        private readonly IProductQueries _productQueries;
        public GetProductsQueryHandler(IProductQueries productQueries, IMapper mapper, IAppCache cache, CacheKeyConstants cacheKeyConstants)
        {
            _productQueries = productQueries;
            _mapper = mapper;
            _cache = cache;
            _cacheKeyConstants = cacheKeyConstants;
        }

        public async Task<(List<ProductResponseModel>, PaginationMetaData)> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var key = _cacheKeyConstants.ProductCacheKey;

            var products = await _cache.GetOrAddAsync(
                key,
                async () => await _productQueries.GetAllProductsAsync(),
                TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours)
                );
            _cacheKeyConstants.AddKeyToList(key);
            products = HandleProductQuery(request.ProductQuery, products);
            var (productsPaginated, pagination) = PaginationHelpers.GetPaginationModel(products, request.Pagination);
            var result = _mapper.Map<List<ProductResponseModel>>(productsPaginated);
            return (result, pagination);
        }

        private List<Product> HandleProductQuery(ProductQuery productQuery, List<Product> products)
        {
            //IsOnStock
            if (productQuery.IsOnStock != null)
            {
                products = products
                    .Where(p => p.IsOnStock == productQuery.IsOnStock)
                    .ToList();
            }
            //Type
            if (productQuery.Types != null && productQuery.Types.Count > 0)
            {
                var productHolder = new List<Product>();
                foreach (var type in productQuery.Types)
                {
                    var result = products
                        .Where(p => p.Type.Name.ToLower().Trim().RemoveDiacritics()
                            .Contains(type.ToLower().Trim().RemoveDiacritics()))
                        .ToList();
                    productHolder.AddRange(result);
                }
                products = productHolder;
            }
            //Brand
            if (productQuery.Brands != null && productQuery.Brands.Count > 0)
            {
                var productHolder = new List<Product>();
                foreach (var brand in productQuery.Brands)
                {
                    var result = products
                        .Where(p => p.Model.Brand.Name.ToLower().Trim().RemoveDiacritics()
                            .Contains(brand.ToLower().Trim().RemoveDiacritics()))
                        .ToList();
                    productHolder.AddRange(result);
                }
                products = productHolder;
            }
            //Model
            if (productQuery.Models != null && productQuery.Models.Count > 0)
            {
                var productHolder = new List<Product>();
                foreach (var model in productQuery.Models)
                {
                    var result = products
                        .Where(p => p.Model.Name.ToLower().Trim().RemoveDiacritics()
                            .Contains(model.ToLower().Trim().RemoveDiacritics()))
                        .ToList();
                    productHolder.AddRange(result);
                }
                products = productHolder;
            }
            //Color
            if (productQuery.Colors != null && productQuery.Colors.Count > 0)
            {
                var productHolder = new List<Product>();
                foreach (var color in productQuery.Colors)
                {
                    var result = products
                        .Where(p => p.Color.Name.ToLower().Trim().RemoveDiacritics()
                            .Contains(color.ToLower().Trim().RemoveDiacritics()))
                        .ToList();
                    productHolder.AddRange(result);
                }
                products = productHolder;
            }
            //Search
            if (!productQuery.Search.IsNullOrEmpty())
            {
                string trimmedSearch = productQuery.Search.Trim().ToLower().RemoveDiacritics();
                string[] searchTerms = trimmedSearch.Split(' ');
                products = products.Where(x =>
                    searchTerms.Any(s =>
                        x.Type.Name.Trim().ToLower().RemoveDiacritics().Contains(s) ||
                        x.Model.Brand.Name.Trim().ToLower().RemoveDiacritics().Contains(s) ||
                        x.Model.Name.Trim().ToLower().RemoveDiacritics().Contains(s) ||
                        x.Color.Name.Trim().ToLower().RemoveDiacritics().Contains(s) ||
                        x.Variant.Trim().ToLower().RemoveDiacritics().Contains(s)
                    ))
                    .ToList();
            }
            return products;
        }
    }
}
