using MediatR;
using T_Shop.Application.Features.Products.ViewModels;

namespace T_Shop.Application.Features.Products.Queries.GetProductsById
{
    public class GetProductByIdQuery : IRequest<ProductDtos>
    {
        public Guid productId { get; set; }
    }
}
