using MediatR;
using T_Shop.Shared.DTOs.Category;

namespace T_Shop.Application.Features.Categories.Queries.GetCategories
{
    public record GetCategoriesQuery : IRequest<List<CategoryDto>>
    {
    }
}
