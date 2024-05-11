using MediatR;
using T_Shop.Application.Features.Products.ViewModels;

namespace T_Shop.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ProductDtos>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}
