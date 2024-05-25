namespace T_Shop.Client.MVC.Services.Services
{
    public class BaseRepository
    {
        Uri baseUriApi = new Uri("https://localhost:5001");
        protected readonly HttpClient _httpClient;
        public BaseRepository(HttpClient httpClient)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseUriApi;
        }
    }
}
