using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using T_Shop.Shared.DTOs.Product;

namespace T_Shop.Client.MVC.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ProductDto> productList = new List<ProductDto>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "api/product").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                productList = JsonConvert.DeserializeObject<List<ProductDto>>(data);
            }
            return View(productList);
        }
    }
}
