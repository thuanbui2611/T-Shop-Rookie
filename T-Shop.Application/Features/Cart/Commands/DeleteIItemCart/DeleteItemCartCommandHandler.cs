using MediatR;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.Features.Cart.Commands.DeleteIItemCart;
public class DeleteItemCartCommandHandler : IRequestHandler<DeleteItemCartCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<CartItem> _cartItemRepository;
    private readonly ICartQueries _cartQueries;

    public DeleteItemCartCommandHandler(IUnitOfWork unitOfWork, IGenericRepository<CartItem> cartItemRepository, ICartQueries cartQueries)
    {
        _unitOfWork = unitOfWork;
        _cartItemRepository = cartItemRepository;
        _cartQueries = cartQueries;
    }

    public async Task<bool> Handle(DeleteItemCartCommand request, CancellationToken cancellationToken)
    {
        var cartID = await _cartQueries.GetCartIdByUserIdAsync(request.UserID);
        if (cartID.Equals(Guid.Empty))
        {
            throw new BadRequestException("Cart not found");
        }

        var itemInCart = await _cartItemRepository.FindOne(x => x.ProductID.Equals(request.ProductID) && x.CartID.Equals(cartID));
        if (itemInCart == null)
        {
            throw new BadRequestException("Item not found in cart");
        }

        _cartItemRepository.DeleteByEntity(itemInCart);

        await _unitOfWork.CompleteAsync();
        _unitOfWork.Detach(itemInCart);

        return true;
    }
}
