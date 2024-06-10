using AutoMapper;
using MediatR;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.SharedServices.StripeService;
using T_Shop.Shared.DTOs.Order.ResponseModel;

namespace T_Shop.Application.Features.Order.Commands.CreateOrUpdateOrder;
public class CreateOrUpdateOrderCommandHandler : IRequestHandler<CreateOrUpdateOrderCommand, OrderResponseModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStripeService _stripeService;
    private readonly IOrderQueries _orderQueries;
    private readonly IUserQueries _userQueries;
    private readonly IGenericRepository<Domain.Entity.Order> _orderRepository;
    private readonly IGenericRepository<OrderDetail> _orderDetailRepository;
    public CreateOrUpdateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IStripeService stripeService, IOrderQueries orderQueries, IUserQueries userQueries)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _stripeService = stripeService;
        _orderRepository = unitOfWork.GetBaseRepo<Domain.Entity.Order>();
        _orderQueries = orderQueries;
        _userQueries = userQueries;
        _orderDetailRepository = unitOfWork.GetBaseRepo<OrderDetail>();
    }
    public async Task<OrderResponseModel> Handle(CreateOrUpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderQueries.GetOrderNotPaymentByUserIdAsync(request.UserID, true);
        if (order == null)
        {
            //Create new order
            order = await HandleCreateNewOrder(request);
        }
        else
        {
            //Update existed order
            order = await HandleUpdateOrder(request, order);
        }
        var paymentIntent = await _stripeService.CreateOrUpdatePaymentIntent(order);
        if (paymentIntent.Id != null || paymentIntent.ClientSecret != null)
        {
            //Create new payment
            order.PaymentIntentID = paymentIntent.Id ?? order.PaymentIntentID;
            order.ClientSecret = paymentIntent.ClientSecret ?? order.ClientSecret;
        }

        await _unitOfWork.CompleteAsync();
        _unitOfWork.Detach(order);
        var orderToReturn = await _orderQueries.GetOrderNotPaymentByUserIdAsync(request.UserID, false);
        var result = _mapper.Map<OrderResponseModel>(orderToReturn);
        return result;
    }

    private async Task<Domain.Entity.Order> HandleCreateNewOrder(CreateOrUpdateOrderCommand request)
    {
        var isUserExist = await _userQueries.CheckIfUserExisted(request.UserID);
        if (!isUserExist) throw new NotFoundException("User not found");

        var order = new Domain.Entity.Order
        {
            UserID = request.UserID,
            ShippingAddress = request.ShippingAddress,
            OrderDetails = new List<OrderDetail>()
        };

        foreach (var productInOrder in request.Products)
        {
            order.OrderDetails.Add(new OrderDetail()
            {
                OrderID = order.Id,
                ProductID = productInOrder.ProductID,
                Quantity = productInOrder.Quantity,
                Price = productInOrder.Price
            });
        }

        _orderRepository.Add(order);
        return order;
    }
    private async Task<Domain.Entity.Order> HandleUpdateOrder(CreateOrUpdateOrderCommand request, Domain.Entity.Order order)
    {
        order.OrderDetails = new List<OrderDetail>();

        foreach (var productInOrder in request.Products)
        {
            order.OrderDetails.Add(new OrderDetail()
            {
                OrderID = order.Id,
                ProductID = productInOrder.ProductID,
                Quantity = productInOrder.Quantity,
                Price = productInOrder.Price
            });
        }
        _orderRepository.Update(order);
        return order;
    }

}
