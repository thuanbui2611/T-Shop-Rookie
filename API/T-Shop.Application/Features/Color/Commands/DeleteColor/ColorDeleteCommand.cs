using MediatR;

namespace T_Shop.Application.Features.Color.Commands.DeleteColor;
public class ColorDeleteCommand : IRequest<bool>
{
    public Guid ID { get; set; }
}
