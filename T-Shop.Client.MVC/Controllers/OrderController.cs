using Microsoft.AspNetCore.Mvc;
using T_Shop.Client.MVC.Repository.Interfaces;
using T_Shop.Shared.DTOs.Order.RequestModel;
using T_Shop.Shared.ViewModels.OrderPage;

namespace T_Shop.Client.MVC.Controllers;
public class OrderController : BaseController
{
    private readonly IOrderRepository _orderRepository;
    private readonly IConfiguration _configuration;
    private readonly ICartRepository _cartRepository;
    public OrderController(IOrderRepository orderRepository, IConfiguration configuration, ICartRepository cartRepository, IUserRepository userRepository) : base(userRepository)
    {
        _orderRepository = orderRepository;
        _configuration = configuration;
        _cartRepository = cartRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var newOrder = _orderRepository.GetOrderOfUser();
        if (newOrder == null)
        {
            return RedirectToPage("Index", "Cart");
        }
        OrderVM OrderViewModel = new OrderVM()
        {
            Order = newOrder,
            StripePublishableKey = _configuration.GetSection("StripeSettings")["PublishableKey"],
            StripeSecretKey = _configuration.GetSection("StripeSettings")["SecretKey"]
        };
        return View(OrderViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderRequestModel order)
    {
        if (CurrentUser == null || !User.Identity.IsAuthenticated)
        {
            TempData["ErrorMessage"] = "You need to login to view your carts!";
            return RedirectToAction("Login", "Authentication");
        }
        order.UserID = CurrentUser.Id;
        order.ShippingAddress = CurrentUser.Address;
        var newOrder = await _orderRepository.CreateOrUpdateOrderAsync(order);
        var redirectUrl = "";
        if (newOrder == null)
        {
            redirectUrl = Url.Action("Index", "Cart");
            return Json(new { redirectUrl });
        }
        redirectUrl = Url.Action("Index", "Order");
        return Json(new { redirectUrl });
    }

    [HttpGet]
    public IActionResult SuccessPayment()
    {
        var order = _orderRepository.GetOrderOfUser();

        _orderRepository.RemoveOrder();
        _cartRepository.ClearCart();
        return View(order);
    }
}
