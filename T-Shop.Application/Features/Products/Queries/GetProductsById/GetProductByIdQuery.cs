using MediatR;
using T_Shop.Shared.DTOs.Product;

namespace T_Shop.Application.Features.Products.Queries.GetProductsById
{
    public record GetProductByIdQuery : IRequest<ProductDto>
    {
        public Guid productId { get; set; }
    }
}
