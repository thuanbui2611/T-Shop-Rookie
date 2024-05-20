using MediatR;
using T_Shop.Shared.DTOs.Cart.RequestModel;
using T_Shop.Shared.DTOs.Cart.ResponseModel;

namespace T_Shop.Application.Features.Cart.Commands.UpdateCart;
public class UpdateCartCommand : CartRequestModel, IRequest<CartResponseModel>
{
}
