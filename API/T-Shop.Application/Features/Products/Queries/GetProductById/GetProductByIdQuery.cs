using MediatR;
using T_Shop.Shared.DTOs.Product.ResponseModel;

namespace T_Shop.Application.Features.Products.Queries.GetProductsById
{
    public record GetProductByIdQuery : IRequest<ProductResponseModel>
    {
        public Guid productId { get; set; }
    }
}
