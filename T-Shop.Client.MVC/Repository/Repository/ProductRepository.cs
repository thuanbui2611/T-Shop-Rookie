using Newtonsoft.Json;
using T_Shop.Client.MVC.Services.Interfaces;
using T_Shop.Shared.DTOs.Product.ResponseModel;
using T_Shop.Shared.DTOs.ProductReview.ResponseModel;
using T_Shop.Shared.ViewModels.ProductsPage;

namespace T_Shop.Client.MVC.Services.Services
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(HttpClient httpClient, IConfiguration configuration) : base(httpClient, configuration)
        {
        }

        public async Task<ProductResponseModel> GetProductByIdAsync(Guid productId)
        {
            var requestUrl = $"api/product/{productId}";
            HttpResponseMessage response = _httpClient.GetAsync(requestUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<ProductResponseModel>(data);
            }
            return null;
        }


        public async Task<ProductListVM> GetProductsAsync(ProductRequestParam productRequestParams)
        {
            var query = new Dictionary<string, string>
            {
                ["pageNumber"] = productRequestParams.PageNumber.ToString(),
                ["pageSize"] = productRequestParams.PageSize.ToString(),
                ["isOnStock"] = "true",
            };

            if (!string.IsNullOrEmpty(productRequestParams.Search))
            {
                query.Add("search", productRequestParams.Search);
            }

            if (!string.IsNullOrEmpty(productRequestParams.Types))
            {
                var types = productRequestParams.Types.Split(",");
                foreach (var type in types)
                {
                    query.Add("types", type);
                }
            }

            if (!string.IsNullOrEmpty(productRequestParams.Brands))
            {
                var brands = productRequestParams.Brands.Split(",");
                foreach (var brand in brands)
                {
                    query.Add("brands", brand);
                }
            }

            if (!string.IsNullOrEmpty(productRequestParams.Models))
            {
                var models = productRequestParams.Models.Split(",");
                foreach (var model in models)
                {
                    query.Add("models", model);
                }
            }

            if (!string.IsNullOrEmpty(productRequestParams.Colors))
            {
                var colors = productRequestParams.Colors.Split(",");
                foreach (var color in colors)
                {
                    query.Add("colors", color);
                }
            }

            var queryString = string.Join("&", query.Select(x => $"{x.Key}={x.Value}"));
            var requestUrl = $"api/product?{queryString}";
            HttpResponseMessage response = _httpClient.GetAsync(requestUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                string productsString = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ProductResponseModel>>(productsString);

                ProductListVM productVM = new ProductListVM()
                {
                    Products = products,
                };

                if (response.Headers.TryGetValues("X-Pagination", out IEnumerable<string> paginationValues))
                {
                    string paginationHeaderValue = paginationValues.FirstOrDefault();
                    var pagination = JsonConvert.DeserializeObject<MetaData>(paginationHeaderValue);
                    //Set value pagination
                    productVM.PaginationMetaData = pagination;
                }

                return productVM;
            }
            return new ProductListVM();
        }

        public async Task<ProductReviewListVM> GetProductReviewsByIdAsync(ProductReviewRequestParam productReviewRequestParams, Guid productId)
        {
            var query = new Dictionary<string, string>
            {
                ["pageNumber"] = productReviewRequestParams.PageNumber.ToString(),
                ["pageSize"] = productReviewRequestParams.PageSize.ToString(),
            };

            var queryString = string.Join("&", query.Select(x => $"{x.Key}={x.Value}"));
            var requestUrl = $"api/product/review/{productId}?{queryString}";
            HttpResponseMessage response = _httpClient.GetAsync(requestUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                string productReviewsString = await response.Content.ReadAsStringAsync();
                var productReviews = JsonConvert.DeserializeObject<List<ProductReviewResponseModel>>(productReviewsString);

                ProductReviewListVM productReviewVM = new ProductReviewListVM()
                {
                    ProductReviews = productReviews,
                };

                if (response.Headers.TryGetValues("X-Pagination", out IEnumerable<string> paginationValues))
                {
                    string paginationHeaderValue = paginationValues.FirstOrDefault();
                    var pagination = JsonConvert.DeserializeObject<MetaData>(paginationHeaderValue);
                    productReviewVM.PaginationMetaData = pagination;
                }

                return productReviewVM;
            }
            return new ProductReviewListVM();


        }


    }
}
