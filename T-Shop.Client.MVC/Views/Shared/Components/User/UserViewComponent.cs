using Microsoft.AspNetCore.Mvc;
using T_Shop.Client.MVC.Repository.Interfaces;

namespace T_Shop.Client.MVC.Views.Shared.Components.User;

public class UserViewComponent : ViewComponent
{
    private readonly IUserRepository _userRepository;

    public UserViewComponent(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = _userRepository.GetCurrentUser(HttpContext);
        return View(user);
    }
}
