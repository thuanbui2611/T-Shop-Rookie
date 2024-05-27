using Newtonsoft.Json;
using T_Shop.Client.MVC.Services.Interfaces;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;

namespace T_Shop.Client.MVC.Services.Services
{
    public class ModelRepository : BaseRepository, IModelRepository
    {
        public ModelRepository(HttpClient httpClient, IConfiguration configuration) : base(httpClient, configuration)
        {

        }

        public async Task<List<ModelProductResponseModel>> GetModelsAsync()
        {
            var requestUrl = "api/model";
            HttpResponseMessage response = _httpClient.GetAsync(requestUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ModelProductResponseModel>>(data);
            }

            return [];
        }
    }
}
