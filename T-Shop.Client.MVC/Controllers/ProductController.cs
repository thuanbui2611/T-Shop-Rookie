using Microsoft.AspNetCore.Mvc;

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
            //List<ProductResponseModel> productList = new List<ProductResponseModel>();
            //HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "api/product").Result;

            //if (response.IsSuccessStatusCode)
            //{
            //    string data = response.Content.ReadAsStringAsync().Result;
            //    productList = JsonConvert.DeserializeObject<List<ProductResponseModel>>(data);
            //}
            //return View(productList);
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
