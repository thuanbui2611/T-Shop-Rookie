using MediatR;

namespace T_Shop.Application.Features.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
