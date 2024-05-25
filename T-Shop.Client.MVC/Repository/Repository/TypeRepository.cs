using Newtonsoft.Json;
using T_Shop.Client.MVC.Services.Interfaces;
using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.Client.MVC.Services.Services
{
    public class TypeRepository : BaseRepository, ITypeRepository
    {
        public TypeRepository(HttpClient httpClient) : base(httpClient)
        {

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
