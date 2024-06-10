using Newtonsoft.Json;
using System.Net.Http.Headers;
using T_Shop.Client.MVC.Repository.Interfaces;
using T_Shop.Shared.DTOs.User.RequestModels;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.Client.MVC.Repository.Repository;

public class UserRepository : IUserRepository
{
    private readonly HttpClient _httpClient;

    public UserRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public UserResponseModel GetCurrentUser(HttpContext httpContext)
    {
        var userString = httpContext.Request.Cookies["CurrentUser"];

        if (!string.IsNullOrEmpty(userString))
        {
            return JsonConvert.DeserializeObject<UserResponseModel>(userString);
        }

        return null;
    }

    public async Task<UserResponseModel> GetUserByIdAsync(Guid UserID)
    {
        var requestUrl = $"api/user/{UserID}";
        HttpResponseMessage response = _httpClient.GetAsync(requestUrl).Result;

        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserResponseModel>(data);
        }
        return null;
    }
    public async Task<UserResponseModel> UpdateUserAsync(UserUpdateRequestModel userToUpdate)
    {
        var requestUrl = $"api/user/{userToUpdate.ID}";

        var formData = new MultipartFormDataContent();
        // Add the review properties to the formData
        formData.Add(new StringContent(userToUpdate.ID.ToString()), "ID");
        formData.Add(new StringContent(userToUpdate.FullName), "FullName");
        formData.Add(new StringContent(userToUpdate.Username), "Username");
        formData.Add(new StringContent(userToUpdate.Email), "Email");
        formData.Add(new StringContent(userToUpdate.DateOfBirth.ToString("yyyy-MM-dd")), "DateOfBirth");
        formData.Add(new StringContent(userToUpdate.Gender), "Gender");
        formData.Add(new StringContent(userToUpdate.PhoneNumber), "PhoneNumber");
        formData.Add(new StringContent(userToUpdate.Address), "Address");
        // Add the files to the formData
        if (userToUpdate.AvatarUpload != null)
        {
            var fileContent = new StreamContent(userToUpdate.AvatarUpload.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(userToUpdate.AvatarUpload.ContentType);
            formData.Add(fileContent, "AvatarUpload", userToUpdate.AvatarUpload.FileName);
        }


        HttpResponseMessage response = await _httpClient.PutAsync(requestUrl, formData);

        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            var userUpdated = JsonConvert.DeserializeObject<UserResponseModel>(data);

            return userUpdated;
        }
        return null;
    }
}
