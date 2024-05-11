using MediatR;
using T_Shop.Domain.Entity;

namespace T_Shop.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Category>
    {
        public string Name { get; set; }
    }
}
