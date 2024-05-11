using MediatR;
using System.Text.Json.Serialization;
using T_Shop.Domain.Entity;

namespace T_Shop.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<Category>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
