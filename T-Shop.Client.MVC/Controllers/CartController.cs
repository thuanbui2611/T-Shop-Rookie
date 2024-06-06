using Microsoft.AspNetCore.Mvc;
using T_Shop.Client.MVC.Repository.Interfaces;
using T_Shop.Shared.DTOs.Cart.RequestModel;
using T_Shop.Shared.DTOs.Cart.ResponseModel;
using T_Shop.Shared.ViewModels.CartPage;

namespace T_Shop.Client.MVC.Controllers;

public class CartController : BaseController
{
    private readonly ICartRepository _cartRepository;
    public CartController(ICartRepository cartRepository, IUserRepository userRepository) : base(userRepository)
    {
        _cartRepository = cartRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (!User.Identity.IsAuthenticated || CurrentUser == null)
        {
            TempData["ErrorMessage"] = "You need to login to view your carts!";
            return RedirectToAction("Login", "Authentication");

        }
        var currentCart = await _cartRepository.GetCurrentCart(CurrentUser.Id);
        CartVM cartVM = new CartVM()
        {
            Cart = currentCart,
            CurrentUser = CurrentUser
        };
        return View(cartVM);
    }

    [HttpGet]
    public async Task<IActionResult> GetCartViewComponent()
    {
        return ViewComponent("Cart");
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(Guid productId)
    {
        if (!User.Identity.IsAuthenticated || CurrentUser == null)
        {
            TempData["ErrorMessage"] = "You need to login to add to cart!";
            //return RedirectToAction("Login", "Authentication");
            return Json(new { redirect = Url.Action("Login", "Authentication") });
        }
        CartRequestModel newItem = new CartRequestModel()
        {
            ProductID = productId,
            Quantity = 1,
            UserID = CurrentUser.Id
        };
        var newCart = await _cartRepository.AddToCartAsync(newItem);
        return newCart != null ? Json(new { success = true }) : Json(new { success = false });
    }

    [HttpPut]
    public async Task<CartResponseModel> UpdateCartItem(Guid productId, int quantity)
    {
        CartRequestModel updatedItem = new CartRequestModel()
        {
            ProductID = productId,
            Quantity = quantity,
            UserID = CurrentUser.Id
        };

        return await _cartRepository.UpdateCartItemAsync(updatedItem);
    }

    [HttpDelete]
    public async Task<bool> DeleteCartItem(Guid productId)
    {

        CartItemDeleteRequestModel itemDeleted = new CartItemDeleteRequestModel()
        {
            ProductID = productId,
            UserID = CurrentUser.Id
        };

        return await _cartRepository.DeleteCartItemAsync(itemDeleted);
    }

    [HttpDelete]
    public void ClearCart()
    {
        _cartRepository.ClearCart();
    }
}
