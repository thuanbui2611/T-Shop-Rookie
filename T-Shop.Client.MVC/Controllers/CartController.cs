using Microsoft.AspNetCore.Mvc;
using T_Shop.Client.MVC.Repository.Interfaces;
using T_Shop.Shared.DTOs.Cart.RequestModel;
using T_Shop.Shared.DTOs.Cart.ResponseModel;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.Client.MVC.Controllers;

public class CartController : Controller
{
    private readonly ICartRepository _cartRepository;
    public CartController(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = HttpContext.Items["CurrentUser"] as UserResponseModel;
        if (!User.Identity.IsAuthenticated || user == null)
        {
            TempData["ErrorMessage"] = "You need to login to view your carts!";
            return RedirectToAction("Login", "Authentication");

        }
        var currentCart = _cartRepository.GetCart();
        if (currentCart == null)
        {
            currentCart = await _cartRepository.GetCartByUserIdAsync(user.Id);
        }

        return View(currentCart);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(Guid productId)
    {
        var user = HttpContext.Items["CurrentUser"] as UserResponseModel;
        if (!User.Identity.IsAuthenticated || user == null)
        {
            TempData["ErrorMessage"] = "You need to login to view your carts!";
            return RedirectToAction("Login", "Authentication");

        }
        CartRequestModel newItem = new CartRequestModel()
        {
            ProductID = productId,
            Quantity = 1,
            UserID = user.Id
        };
        var newCart = await _cartRepository.AddToCartAsync(newItem);
        return newCart != null ? Json(new { success = true }) : Json(new { success = false });
    }

    [HttpPut]
    public async Task<CartResponseModel> UpdateCartItem(Guid productId, int quantity)
    {
        var user = HttpContext.Items["CurrentUser"] as UserResponseModel;
        CartRequestModel updatedItem = new CartRequestModel()
        {
            ProductID = productId,
            Quantity = quantity,
            UserID = user.Id
        };

        return await _cartRepository.UpdateCartItemAsync(updatedItem);
    }

    [HttpDelete]
    public async Task<bool> DeleteCartItem(Guid productId)
    {
        var user = HttpContext.Items["CurrentUser"] as UserResponseModel;
        CartItemDeleteRequestModel itemDeleted = new CartItemDeleteRequestModel()
        {
            ProductID = productId,
            UserID = user.Id
        };

        return await _cartRepository.DeleteCartItemAsync(itemDeleted);
    }

    [HttpDelete]
    public void ClearCart()
    {
        _cartRepository.ClearCart();
    }
}
