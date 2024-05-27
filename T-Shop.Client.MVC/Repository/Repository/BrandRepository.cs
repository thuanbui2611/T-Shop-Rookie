using Newtonsoft.Json;
using T_Shop.Client.MVC.Services.Interfaces;
using T_Shop.Shared.DTOs.Brand.ResponseModel;

namespace T_Shop.Client.MVC.Services.Services
{
    public class BrandRepository : BaseRepository, IBrandRepository
    {
        public BrandRepository(HttpClient httpClient, IConfiguration configuration) : base(httpClient, configuration)
        {

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
