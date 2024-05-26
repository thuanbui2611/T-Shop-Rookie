using Microsoft.AspNetCore.Mvc;
using T_Shop.Client.MVC.Repository.Interfaces;
using T_Shop.Shared.DTOs.User.RequestModels;

namespace T_Shop.Client.MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationController(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserAuthenRequestModel user)
        {
            var userLogin = await _authenticationRepository.Login(user);

            if (userLogin != null)
            {
                Response.Cookies.Append("AuthToken", userLogin.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");
            return RedirectToAction("Index", "Home");
        }

    }
}
