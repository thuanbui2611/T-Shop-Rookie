using AutoMapper;
using MediatR;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Cart.ResponseModel;

namespace T_Shop.Application.Features.Cart.Commands.UpdateCart;
public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, CartResponseModel>
{

    private readonly ICartQueries _cartQueries;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Domain.Entity.Cart> _cartRepository;
    private readonly IGenericRepository<CartItem> _cartItemRepository;
    private readonly IGenericRepository<Product> _productRepository;

    public UpdateCartCommandHandler(ICartQueries cartQueries, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _cartQueries = cartQueries;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<CartResponseModel> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
    {
        var cartID = await _cartQueries.GetCartIdByUserIdAsync(request.UserID);
        if (cartID.Equals(Guid.Empty))
        {
            throw new BadRequestException("Cart not found");
        }
        else
        {
            var isProductInCart = await _cartQueries.CheckIfItemExistedInCart(cartID, request.ProductID);
            if (isProductInCart)
            {
                throw new BadRequestException("Product in cart not found");
            }

            var cartItem = await _cartItemRepository.FindOne(x => x.CartID.Equals(cartID) && x.ProductID.Equals(request.ProductID));
            var productOfCart = await _productRepository.GetById(cartItem.ProductID);
            if (cartItem.Quantity > request.Quantity)
            {
                cartItem.Quantity = request.Quantity;
            }
            else throw new BadRequestException($"The product only has {cartItem.Quantity} on stock");
            _cartItemRepository.Update(cartItem);
            await _unitOfWork.CompleteAsync();
            _unitOfWork.Detach(cartItem);
        }
        var cart = await _cartQueries.GetCartByIdAsync(cartID);
        var result = _mapper.Map<CartResponseModel>(cart);
        return result;
    }
}
