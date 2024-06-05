using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using T_Shop.Client.MVC.Repository.Interfaces;
using T_Shop.Shared.DTOs.User.RequestModels;

namespace T_Shop.Client.MVC.Controllers;
public class UserController : BaseController
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository) : base(userRepository)
    {
        _userRepository = userRepository;
    }

    public IActionResult Setting()
    {
        return View(CurrentUser);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateUser(UserUpdateRequestModel userToUpdate)
    {
        userToUpdate.ID = CurrentUser.Id;
        string gender = Request.Form["genderSelect"];
        userToUpdate.Gender = gender;
        var userUpdated = await _userRepository.UpdateUserAsync(userToUpdate);

        CleanCurrentUser();

        Response.Cookies.Append("CurrentUser", JsonConvert.SerializeObject(userUpdated), new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
        });

        return RedirectToAction("Setting");
    }
}
