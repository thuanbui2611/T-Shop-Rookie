using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            return RedirectToAction("login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            var errorMessage = TempData["ErrorMessage"]?.ToString();
            ViewBag.ErrorMessage = errorMessage;
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.ErrorMessage = "You have logined!";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserAuthenRequestModel user)
        {
            var (token, userLogin) = await _authenticationRepository.Login(user);

            if (token != null && userLogin != null)
            {
                Response.Cookies.Append("AuthToken", token.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                Response.Cookies.Append("CurrentUser", JsonConvert.SerializeObject(userLogin), new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                });
                TempData["SuccessMessage"] = "Login Success!";
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserCreationResquestModel request)
        {
            string gender = Request.Form["genderSelect"];
            request.Gender = gender;

            var result = await _authenticationRepository.Register(request);
            if (result)
            {
                TempData["SuccessMessage"] = "Login Success!";
                return RedirectToAction("Login", "Authentication");
            }

            TempData["FailedMessage"] = "Register failed!";
            return View();
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");
            Response.Cookies.Delete("CurrentUser");
            TempData["SuccessMessage"] = "Logout Success!";
            return RedirectToAction("Index", "Home");
        }

    }
}
