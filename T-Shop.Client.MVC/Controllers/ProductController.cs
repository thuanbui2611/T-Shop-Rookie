using Microsoft.AspNetCore.Mvc;
using T_Shop.Client.MVC.Services.Interfaces;
using T_Shop.Shared.ViewModels.ProductsPage;

namespace T_Shop.Client.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(ProductRequestParam? productRequestParam)
        {


            var products = await _productService.GetProductsAsync(productRequestParam);
            return View(products);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return View(product);
        }
    }
}
