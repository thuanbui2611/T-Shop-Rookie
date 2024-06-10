using MediatR;

namespace T_Shop.Application.Features.Type.Commands.DeleteType;
public class DeleteTypeCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
