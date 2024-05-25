namespace T_Shop.Client.MVC.Services.Services
{
    public class BaseRepository
    {
        Uri baseUriApi = new Uri("https://648c-116-110-41-101.ngrok-free.app");
        protected readonly HttpClient _httpClient;
        public BaseRepository(HttpClient httpClient)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseUriApi;
        }
    }
}
