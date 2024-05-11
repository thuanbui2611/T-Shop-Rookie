using MediatR;
using T_Shop.Domain.Entity;

namespace T_Shop.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<Category>
    {
        public Guid Id { get; set; }
    }
}
