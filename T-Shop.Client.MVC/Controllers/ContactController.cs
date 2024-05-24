using Microsoft.AspNetCore.Mvc;

namespace T_Shop.Client.MVC.Controllers;
public class ContactController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
