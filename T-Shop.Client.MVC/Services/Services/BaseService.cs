namespace T_Shop.Client.MVC.Services.Services
{
    public class BaseService
    {
        Uri baseUriApi = new Uri("https://localhost:5001");
        protected readonly HttpClient _httpClient;
        public BaseService(HttpClient httpClient)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseUriApi;
        }
    }
}
