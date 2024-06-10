using MediatR;
using T_Shop.Shared.DTOs.Order.RequestModel;
using T_Shop.Shared.DTOs.Order.ResponseModel;

namespace T_Shop.Application.Features.Order.Commands.CreateOrUpdateOrder;
public class CreateOrUpdateOrderCommand : OrderRequestModel, IRequest<OrderResponseModel>
{
}
