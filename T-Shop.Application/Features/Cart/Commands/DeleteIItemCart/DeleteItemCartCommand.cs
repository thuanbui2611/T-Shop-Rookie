using MediatR;
using T_Shop.Shared.DTOs.Cart.RequestModel;

namespace T_Shop.Application.Features.Cart.Commands.DeleteIItemCart;
public class DeleteItemCartCommand : CartItemDeleteRequestModel, IRequest<bool>
{
}
