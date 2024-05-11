using Microsoft.AspNetCore.Mvc;

namespace T_Shop.Client.MVC.Controllers
{
    public class ControllerBase : Controller
    {
        Uri baseUriApi = new Uri("https://localhost:5001");

        protected readonly HttpClient _httpClient;

        public ControllerBase()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseUriApi;
        }
    }
}
