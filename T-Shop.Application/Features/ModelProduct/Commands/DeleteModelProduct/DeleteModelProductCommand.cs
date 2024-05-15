using MediatR;

namespace T_Shop.Application.Features.ModelProduct.Commands.DeleteModelProduct;
public class DeleteModelProductCommand : IRequest<bool>
{
    public Guid ID { get; set; }
}
