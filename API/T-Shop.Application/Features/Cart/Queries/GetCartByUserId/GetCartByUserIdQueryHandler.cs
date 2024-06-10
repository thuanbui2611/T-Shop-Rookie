using AutoMapper;
using MediatR;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Cart.ResponseModel;

namespace T_Shop.Application.Features.Cart.Queries.GetCartByUserId;
public class GetCartByUserIdQueryHandler : IRequestHandler<GetCartByUserIdQuery, CartResponseModel>
{
    private readonly IMapper _mapper;
    private readonly ICartQueries _cartQueries;

    public GetCartByUserIdQueryHandler(IMapper mapper, ICartQueries cartQueries)
    {
        _mapper = mapper;
        _cartQueries = cartQueries;
    }

    public async Task<CartResponseModel> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
    {
        var cart = await _cartQueries.GetCartByUserIdAsync(request.UserID);
        var result = _mapper.Map<CartResponseModel>(cart);
        return result;
    }
}
