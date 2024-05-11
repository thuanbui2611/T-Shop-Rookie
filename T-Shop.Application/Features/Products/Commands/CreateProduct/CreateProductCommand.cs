using MediatR;
using T_Shop.Shared.DTOs.Product;

namespace T_Shop.Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand : IRequest<ProductDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}
