using MediatR;
using T_Shop.Domain.Entity;
using T_Shop.Shared.DTOs.Category;

namespace T_Shop.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : CategoryRequestModel, IRequest<Category>
    {
        //public string Name { get; set; }
    }
}
