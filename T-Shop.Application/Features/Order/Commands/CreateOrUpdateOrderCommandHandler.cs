using AutoMapper;
using MediatR;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.SharedServices.StripeService;
using T_Shop.Shared.DTOs.Order.ResponseModel;

namespace T_Shop.Application.Features.Order.Commands;
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
        var order = await _orderQueries.GetOrderByUserIdAsync(request.UserID);

        if (order == null)
        {
            //Create new order
            order = await HandleCreateNewOrder(request, order);
        }
        else
        {
            //Update order
            order = await HandleUpdateOrder(request, order);
        };
        _unitOfWork.Detach(order);

        //var lastestOrder = await _orderQueries.GetOrderByUserIdAsync(order.UserID);
        var paymentIntent = await _stripeService.CreateOrUpdatePaymentIntent(order);
        order.PaymentIntentID = paymentIntent.Id ?? order.PaymentIntentID;
        order.ClientSecret = paymentIntent.ClientSecret ?? order.ClientSecret;

        _orderRepository.Update(order);
        await _unitOfWork.CompleteAsync();

        var result = _mapper.Map<OrderResponseModel>(order);
        return result;
    }

    private async Task<Domain.Entity.Order> HandleCreateNewOrder(CreateOrUpdateOrderCommand request, Domain.Entity.Order order)
    {
        var isUserExist = await _userQueries.CheckIfUserExisted(request.UserID);
        if (!isUserExist) throw new NotFoundException("User not found");

        order = new Domain.Entity.Order
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
        _orderDetailRepository.AddRange(order.OrderDetails);
        await _unitOfWork.CompleteAsync();
        return order;
    }
    private async Task<Domain.Entity.Order> HandleUpdateOrder(CreateOrUpdateOrderCommand request, Domain.Entity.Order order)
    {
        _orderDetailRepository.DeleteRange(order.OrderDetails);
        //order.OrderDetails.Clear();

        order.OrderDetails = new List<OrderDetail>();

        foreach (var productInOrder in request.Products)
        {
            order.OrderDetails.Add(new OrderDetail()
            {
                OrderID = order.Id,
                ProductID = productInOrder.ProductID,
                Quantity = productInOrder.Quantity
            });
        }

        _orderDetailRepository.AddRange(order.OrderDetails);
        await _unitOfWork.CompleteAsync();
        return order;
    }

}
