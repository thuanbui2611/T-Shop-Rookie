using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using T_Shop.Client.MVC.Models;

namespace T_Shop.Client.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var errorMessage = TempData["ErrorMessage"]?.ToString();
            var successMessage = TempData["SuccessMessage"]?.ToString();
            ViewBag.ErrorMessage = errorMessage;
            ViewBag.SuccessMessage = successMessage;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
