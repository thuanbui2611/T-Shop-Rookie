using Microsoft.Extensions.Configuration;
using Stripe;
using T_Shop.Domain.Entity;

namespace T_Shop.Infrastructure.SharedServices.StripeService;
public class StripeService : IStripeService
{
    private readonly string _secretKey;
    public StripeService(IConfiguration configuration)
    {
        _secretKey = configuration["StripeSettings:SecretKey"];
    }

    public async Task<PaymentIntent> CreateOrUpdatePaymentIntent(Order order)
    {
        StripeConfiguration.ApiKey = _secretKey;

        var service = new PaymentIntentService();

        var intent = new PaymentIntent();
        var subTotal = order.OrderDetails.Sum(orderDetail => orderDetail.Quantity * orderDetail.Price);
        if (string.IsNullOrEmpty(order.PaymentIntentID))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = decimal.ToInt64(subTotal),
                Currency = "vnd",
                PaymentMethodTypes = new List<string> { "card" }
            };

            intent = await service.CreateAsync(options);
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = decimal.ToInt64(subTotal),
                Currency = "vnd"
            };
            await service.UpdateAsync(order.PaymentIntentID, options);
        }

        return intent;
    }

    public async Task<Refund> RefundPayment(string paymentIntentId, decimal ammounts, string reason)
    {
        StripeConfiguration.ApiKey = _secretKey;

        var paymentInentService = new PaymentIntentService();

        var paymentIntent = await paymentInentService.GetAsync(paymentIntentId);

        var options = new RefundCreateOptions
        {
            Charge = paymentIntent.LatestChargeId,
            Amount = (long?)ammounts,
            Reason = "requested_by_customer"
        };

        var refundService = new RefundService();

        return await refundService.CreateAsync(options);
    }
}
