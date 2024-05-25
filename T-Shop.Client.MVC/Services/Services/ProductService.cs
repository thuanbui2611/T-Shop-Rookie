using Newtonsoft.Json;
using T_Shop.Client.MVC.Services.Interfaces;
using T_Shop.Shared.DTOs.Product.ResponseModel;
using T_Shop.Shared.ViewModels.ProductsPage;

namespace T_Shop.Client.MVC.Services.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<ProductResponseModel> GetProductByIdAsync(Guid productId)
        {
            var requestUrl = $"api/product/{productId}";
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + requestUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<ProductResponseModel>(data);
            }
            return null;
        }

        public async Task<List<ProductResponseModel>> GetProductsAsync(ProductRequestParam productRequestParams)
        {
            var query = new Dictionary<string, string>
            {
                ["pageNumber"] = productRequestParams.PageNumber.ToString(),
                ["pageSize"] = productRequestParams.PageSize.ToString(),
            };
            if (productRequestParams.Search != null)
            {
                query.Add("search", productRequestParams.Search.ToString());
            }

            foreach (var brand in productRequestParams.Brands ?? new List<string>())
            {
                query.Add("brands", brand);
            }
            foreach (var model in productRequestParams.Models ?? new List<string>())
            {
                query.Add("models", model);
            }
            foreach (var type in productRequestParams.Types ?? new List<string>())
            {
                query.Add("types", type);
            }
            foreach (var color in productRequestParams.Colors ?? new List<string>())
            {
                query.Add("colors", color);
            }

            var queryString = string.Join("&", query.Select(x => $"{x.Key}={x.Value}"));
            var requestUrl = $"api/product?{queryString}";
            HttpResponseMessage response = _httpClient.GetAsync(requestUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductResponseModel>>(data);
            }
            return [];
        }
    }
}
