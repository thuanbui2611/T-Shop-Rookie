using Microsoft.AspNetCore.Mvc;
using T_Shop.Client.MVC.Services.Interfaces;
using T_Shop.Shared.ViewModels.ProductsPage;

namespace T_Shop.Client.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ITypeRepository _typeRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IColorRepository _colorRepository;
        private readonly IModelRepository _modelRepository;

        public ProductController(ILogger<ProductController> logger,
                                 IProductRepository productRepository,
                                 ITypeRepository typeRepository,
                                 IBrandRepository brandRepository,
                                 IColorRepository colorRepository,
                                 IModelRepository modelRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _typeRepository = typeRepository;
            _brandRepository = brandRepository;
            _colorRepository = colorRepository;
            _modelRepository = modelRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var brands = await _brandRepository.GetBrandsAsync();
            var models = await _modelRepository.GetModelsAsync();
            var types = await _typeRepository.GetTypesAsync();
            var colors = await _colorRepository.GetColorsAsync();

            ProductVM viewModel = new ProductVM()
            {
                Brands = brands,
                Colors = colors,
                Models = models,
                Types = types
            };
            return View(viewModel);
        }

        public async Task<IActionResult> ProductListPartial(ProductRequestParam? productRequestParam)
        {

            var products = await _productRepository.GetProductsAsync(productRequestParam);
            return PartialView("_ProductListPartial", products);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            return View(product);
        }



    }
}
