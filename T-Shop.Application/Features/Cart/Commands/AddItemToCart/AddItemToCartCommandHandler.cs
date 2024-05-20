using AutoMapper;
using MediatR;
using T_Shop.Application.Common.Exceptions;
using T_Shop.Application.Features.Cart.Commands.AddItemToCartQuery;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Cart.ResponseModel;

namespace T_Shop.Application.Features.Cart.Commands.AddItemToCart;
public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommand, CartResponseModel>
{
    private readonly ICartQueries _cartQueries;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Domain.Entity.Cart> _cartRepository;
    private readonly IGenericRepository<CartItem> _cartItemRepository;
    private readonly IGenericRepository<Product> _productRepository;

    public AddItemToCartCommandHandler(ICartQueries cartQueries, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _cartQueries = cartQueries;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _cartRepository = unitOfWork.GetBaseRepo<Domain.Entity.Cart>();
        _cartItemRepository = unitOfWork.GetBaseRepo<CartItem>();
        _productRepository = unitOfWork.GetBaseRepo<Product>();
    }

    public async Task<CartResponseModel> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        var cartID = await _cartQueries.GetCartIdByUserIdAsync(request.UserID);

        if (cartID.Equals(Guid.Empty))
        {
            await HandleCreateNewCartAsync(request);
        }
        else
        {
            await HandleOldCartAsync(request, cartID);
        }
        var cart = await _cartQueries.GetCartByUserIdAsync(request.UserID);
        var result = _mapper.Map<CartResponseModel>(cart);
        return result;
    }

    public async Task HandleCreateNewCartAsync(AddItemToCartCommand request)
    {
        if (request.UserID.Equals(Guid.Empty)) throw new BadRequestException("User ID not found");
        var newCart = new Domain.Entity.Cart
        {
            Id = Guid.NewGuid(),
            UserID = request.UserID
        };
        var newCartItem = new CartItem
        {
            CartID = newCart.Id,
            ProductID = request.ProductID,
            Quantity = request.Quantity
        };
        _cartRepository.Add(newCart);
        _cartItemRepository.Add(newCartItem);
        await _unitOfWork.CompleteAsync();
    }

    public async Task HandleOldCartAsync(AddItemToCartCommand request, Guid cartID)
    {
        var itemInCart = await _cartItemRepository.FindOne(x => x.CartID.Equals(cartID) && x.ProductID.Equals(request.ProductID));

        if (itemInCart == null)
        {
            //new item
            itemInCart = new CartItem
            {
                CartID = cartID,
                ProductID = request.ProductID,
                Quantity = request.Quantity
            };
            _cartItemRepository.Add(itemInCart);
        }
        else
        {
            //update quantity of item existed
            var productOfCart = await _productRepository.GetById(itemInCart.ProductID);
            if (productOfCart.Quantity >= request.Quantity)
            {
                itemInCart.Quantity = request.Quantity;
                _cartItemRepository.Update(itemInCart);
            }
            else throw new BadRequestException($"The product only has {productOfCart.Quantity} on stock");
        }

        await _unitOfWork.CompleteAsync();
    }
}
