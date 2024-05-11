using MediatR;
using T_Shop.Shared.DTOs.Product;

namespace T_Shop.Application.Features.Products.Queries.GetProducts
{
    public record GetProductQuery : IRequest<List<ProductDto>>
    {
    }
}
