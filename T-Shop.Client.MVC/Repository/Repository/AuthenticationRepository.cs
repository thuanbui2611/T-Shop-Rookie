using Newtonsoft.Json;
using T_Shop.Client.MVC.Repository.Interfaces;
using T_Shop.Client.MVC.Services.Services;
using T_Shop.Shared.DTOs.User.RequestModels;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.Client.MVC.Repository.Repository
{
    public class AuthenticationRepository : BaseRepository, IAuthenticationRepository
    {
        public AuthenticationRepository(HttpClient httpClient) : base(httpClient)
        {

        }

        public async Task<UserAuthenResponseModel> Login(UserAuthenRequestModel user)
        {
            var requestUrl = "api/authentication/login";
            HttpResponseMessage response = _httpClient.PostAsJsonAsync(requestUrl, user).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserAuthenResponseModel>(data);
            }
            return null;
        }

        public Task<UserResponseModel> Register(UserCreationResquestModel user)
        {
            throw new NotImplementedException();
        }
    }
}
