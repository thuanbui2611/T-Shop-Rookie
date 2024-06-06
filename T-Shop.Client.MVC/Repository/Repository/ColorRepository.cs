using Newtonsoft.Json;
using T_Shop.Client.MVC.Services.Interfaces;
using T_Shop.Shared.DTOs.Color.ResponseModel;

namespace T_Shop.Client.MVC.Services.Services
{
    public class ColorRepository : IColorRepository
    {
        private readonly HttpClient _httpClient;

        public ColorRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ColorResponseModel>> GetColorsAsync()
        {
            var requestUrl = "api/color";
            HttpResponseMessage response = _httpClient.GetAsync(requestUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ColorResponseModel>>(data);
            }

            return [];
        }
    }
}
