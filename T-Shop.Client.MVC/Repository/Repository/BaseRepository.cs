namespace T_Shop.Client.MVC.Services.Services
{
    public class BaseRepository
    {
        protected readonly IConfiguration _configuration;
        protected readonly HttpClient _httpClient;
        public BaseRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(configuration["ApiConnectionString"]);
        }
    }
}
