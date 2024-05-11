using MediatR;
using T_Shop.Shared.DTOs.Category;

namespace T_Shop.Application.Features.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery : IRequest<CategoryDto>
    {
        public Guid Id { get; set; }
    }
}
