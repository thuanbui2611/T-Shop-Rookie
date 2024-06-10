using Stripe;
using T_Shop.Domain.Entity;

namespace T_Shop.Infrastructure.SharedServices.StripeService;
public interface IStripeService
{
    Task<PaymentIntent> CreateOrUpdatePaymentIntent(Order order);
    Task<Refund> RefundPayment(string paymentIntentId, decimal ammounts, string reason);
}
