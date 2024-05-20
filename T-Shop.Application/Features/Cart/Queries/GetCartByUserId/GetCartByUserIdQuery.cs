using MediatR;
using T_Shop.Shared.DTOs.Cart.ResponseModel;

namespace T_Shop.Application.Features.Cart.Queries.GetCartByUserId;
public class GetCartByUserIdQuery : IRequest<CartResponseModel>
{
    public Guid UserID { get; set; }
}
