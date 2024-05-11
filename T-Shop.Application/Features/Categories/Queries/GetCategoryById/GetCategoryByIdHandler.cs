using MediatR;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Entity.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, Category>
    {
        private readonly ICategoryQueries _categoryQueries;

        public GetCategoryByIdHandler(ICategoryQueries categoryQueries)
        {
            _categoryQueries = categoryQueries;
        }

        public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryQueries.GetCategoryByIdAsync(request.Id);
            if (category == null)
            {
                throw new BadRequestException("Can not found category");
            }
            return category;
        }
    }
}
