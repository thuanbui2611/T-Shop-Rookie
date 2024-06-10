using Microsoft.AspNetCore.Mvc;
using T_Shop.Client.MVC.Repository.Interfaces;
using T_Shop.Shared.DTOs.ProductReview.RequestModel;
using T_Shop.Shared.DTOs.ProductReview.ResponseModel;
using T_Shop.Shared.DTOs.Transaction.RequestModel;
using T_Shop.Shared.DTOs.Transaction.ResponseModel;
using T_Shop.Shared.ViewModels.TransactionPage;

namespace T_Shop.Client.MVC.Controllers;
public class MyOrderController : BaseController
{
    private readonly ITransactionRepository _transactionRepository;

    public MyOrderController(ITransactionRepository transactionRepository, IUserRepository userRepository) : base(userRepository)
    {
        _transactionRepository = transactionRepository;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (!User.Identity.IsAuthenticated || CurrentUser == null)
        {
            TempData["ErrorMessage"] = "You need to login to view your orders.";
            return RedirectToAction("Login", "Authentication");
        }
        return View();
    }

    public async Task<IActionResult> TransactionListPartial(TransactionRequestParam transactionRequestParam)
    {
        var transactions = await _transactionRepository.GetTransactionsOfUserAsync(transactionRequestParam, CurrentUser.Id);
        return PartialView("_TransactionListPartial", transactions);
    }

    public async Task<IActionResult> Details()
    {
        var transactionIdRoute = RouteData.Values["id"];
        if (transactionIdRoute == null) return NotFound();

        Guid transactionId;
        var isGuid = Guid.TryParse(transactionIdRoute.ToString(), out transactionId);
        if (!isGuid) return NotFound();

        var transaction = await _transactionRepository.GetTransactionByIdAsync(transactionId);
        return View(transaction);
    }

    [HttpPut]
    public async Task<TransactionResponseModel> UpdateTransactionStatus(TransactionUpdateRequestModel request)
    {
        return await _transactionRepository.UpdateTransactionStatusAsync(request);
    }


    [HttpPost]
    public async Task<ProductReviewResponseModel> ReviewProductInTransaction(ProductReviewCreationRequestModel review)
    {
        review.UserID = CurrentUser.Id;
        return await _transactionRepository.CreateReviewForProduct(review);
    }
}
