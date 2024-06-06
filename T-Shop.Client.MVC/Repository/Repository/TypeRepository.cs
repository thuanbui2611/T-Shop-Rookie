using Newtonsoft.Json;
using T_Shop.Client.MVC.Services.Interfaces;
using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.Client.MVC.Services.Services
{
    public class TypeRepository : ITypeRepository
    {
        private readonly HttpClient _httpClient;

        public TypeRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TypeResponseModel>> GetTypesAsync()
        {
            var requestUrl = "api/type";
            HttpResponseMessage response = _httpClient.GetAsync(requestUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<TypeResponseModel>>(data);
            }

            return [];
        }
    }
}
