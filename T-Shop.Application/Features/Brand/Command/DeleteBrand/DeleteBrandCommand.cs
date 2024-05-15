using MediatR;

namespace T_Shop.Application.Features.Brand.Command.DeleteBrand;
public class DeleteBrandCommand : IRequest<bool>
{
    public Guid ID { get; set; }
}
