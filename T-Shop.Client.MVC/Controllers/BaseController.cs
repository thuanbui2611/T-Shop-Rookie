using Microsoft.AspNetCore.Mvc;
using T_Shop.Client.MVC.Repository.Interfaces;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.Client.MVC.Controllers;

public class BaseController : Controller
{
    private readonly IUserRepository _userRepository;
    private UserResponseModel? _currentUser;

    public BaseController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected UserResponseModel CurrentUser
    {
        get
        {
            if (_currentUser == null)
            {
                _currentUser = _userRepository.GetCurrentUser(HttpContext);
            }
            return _currentUser;
        }
    }

    protected virtual void CleanCurrentUser()
    {
        _currentUser = null;
    }
}