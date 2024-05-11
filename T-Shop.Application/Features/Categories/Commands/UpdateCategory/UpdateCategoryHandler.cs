using AutoMapper;
using MediatR;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Category>
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCategoryHandler(IGenericRepository<Category> categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _categoryRepository.GetById(request.Id);
            if (category == null)
            {
                //throw new StatusCodeException(message: "Category not found", statusCode: StatusCodes.Status404NotFound);
            }
            var categoryUpdated = _mapper.Map<Category>(category);
            _categoryRepository.Update(categoryUpdated);
            await _unitOfWork.CompleteAsync();
            return categoryUpdated;
        }
    }
}
