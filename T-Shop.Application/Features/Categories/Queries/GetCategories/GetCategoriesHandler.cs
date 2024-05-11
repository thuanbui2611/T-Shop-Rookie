using AutoMapper;
using MediatR;
using T_Shop.Application.Features.Categories.DTOs;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDtos>>
    {
        private readonly ICategoryQueries _categoryQueries;
        private readonly IMapper _mapper;

        public GetCategoriesHandler(ICategoryQueries categoryQueries, IMapper mapper)
        {
            _categoryQueries = categoryQueries;
            _mapper = mapper;
        }

        public async Task<List<CategoryDtos>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryQueries.GetCategoriesAsync();
            var results = _mapper.Map<List<CategoryDtos>>(categories);
            return results;
        }
    }
}
