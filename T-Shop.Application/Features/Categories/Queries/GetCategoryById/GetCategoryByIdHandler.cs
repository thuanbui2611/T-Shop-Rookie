using AutoMapper;
using MediatR;
using T_Shop.Domain.Entity.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Category;

namespace T_Shop.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ICategoryQueries _categoryQueries;
        private readonly IMapper _mapper;

        public GetCategoryByIdHandler(ICategoryQueries categoryQueries, IMapper mapper)
        {
            _categoryQueries = categoryQueries;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryQueries.GetCategoryByIdAsync(request.Id);
            if (category == null)
            {
                throw new BadRequestException("Category not found");
            }
            var result = _mapper.Map<CategoryDto>(category);
            return result;
        }
    }
}
