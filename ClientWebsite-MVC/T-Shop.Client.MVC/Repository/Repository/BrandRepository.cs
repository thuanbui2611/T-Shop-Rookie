using Newtonsoft.Json;
using T_Shop.Client.MVC.Services.Interfaces;
using T_Shop.Shared.DTOs.Brand.ResponseModel;

namespace T_Shop.Client.MVC.Services.Services
{
    public class BrandRepository : IBrandRepository
    {
        private readonly HttpClient _httpClient;

        public BrandRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BrandResponseModel>> GetBrandsAsync()
        {
            var requestUrl = "api/brand";
            HttpResponseMessage response = _httpClient.GetAsync(requestUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<BrandResponseModel>>(data);
            }

            return [];
        }
    }
}
