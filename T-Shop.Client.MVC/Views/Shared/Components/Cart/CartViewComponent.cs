using Microsoft.AspNetCore.Mvc;
using T_Shop.Client.MVC.Repository.Interfaces;

namespace T_Shop.Client.MVC.Views.Shared.Components.Cart;

public class CartViewComponent : ViewComponent
{
    private readonly ICartRepository _cartRepository;
    private readonly IUserRepository _userRepository;
    public CartViewComponent(ICartRepository cartRepository, IUserRepository userRepository)
    {
        _cartRepository = cartRepository;
        _userRepository = userRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var currentUser = _userRepository.GetCurrentUser(HttpContext);
        if (currentUser == null)
        {
            return View(0);
        }
        var cart = await _cartRepository.GetCurrentCart(currentUser.Id);

        var cartCount = cart.CartItems == null ? 0 : cart.CartItems.Count();
        return View(cartCount);
    }
}
