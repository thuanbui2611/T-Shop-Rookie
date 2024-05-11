using MediatR;
using T_Shop.Application.Features.Categories.DTOs;

namespace T_Shop.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQuery : IRequest<List<CategoryDtos>>
    {
    }
}
