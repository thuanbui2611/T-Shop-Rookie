using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using T_Shop.Client.MVC.Repository.Interfaces;
using T_Shop.Client.MVC.Services.Services;
using T_Shop.Shared.DTOs.User.RequestModels;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.Client.MVC.Repository.Repository
{
    public class AuthenticationRepository : BaseRepository, IAuthenticationRepository
    {
        public AuthenticationRepository(HttpClient httpClient, IConfiguration configuration) : base(httpClient, configuration)
        {
        }

        public async Task<(UserAuthenResponseModel, UserResponseModel)> Login(UserAuthenRequestModel user)
        {
            var requestUrl = "api/authentication/login";
            HttpResponseMessage response = _httpClient.PostAsJsonAsync(requestUrl, user).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<UserAuthenResponseModel>(data);

                var userData = GetUserInformationFromToken(token.Token);
                return (token, userData);
            }
            return (null, null);
        }

        public async Task<bool> Register(UserCreationResquestModel request)
        {
            var requestUrl = "api/authentication/register";
            HttpResponseMessage response = _httpClient.PostAsJsonAsync(requestUrl, request).Result;
            string test = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        private UserResponseModel GetUserInformationFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken != null)
            {
                var claims = jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value);

                var userId = claims.GetValueOrDefault("UserId");
                var user = new UserResponseModel
                {
                    Id = !userId.IsNullOrEmpty() ? new Guid(userId) : Guid.Empty,
                    FullName = claims.GetValueOrDefault("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"),
                    Email = claims.GetValueOrDefault("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"),
                    Avatar = claims.GetValueOrDefault("Avatar"),
                    Role = claims.GetValueOrDefault("http://schemas.microsoft.com/ws/2008/06/identity/claims/role"),
                    Address = claims.GetValueOrDefault("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress"),
                    PhoneNumber = claims.GetValueOrDefault("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone"),
                    DateOfBirth = DateTime.Parse(claims.GetValueOrDefault("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth")),
                    Username = claims.GetValueOrDefault("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"),
                    Gender = claims.GetValueOrDefault("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender")
                };
                return user;
            }
            return null;
        }
    }
}
