using MediatR;

namespace T_Shop.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
