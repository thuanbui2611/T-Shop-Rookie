using MediatR;

namespace T_Shop.Application.Features.Products.Commands.UpdateProductStatus;
public class LockOrUnlockProductCommand : IRequest<bool>
{
    public Guid ProductID { get; set; }
}
