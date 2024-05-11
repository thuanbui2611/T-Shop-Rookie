using MediatR;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryHandler(IGenericRepository<Category> categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetById(request.Id);
            if (category == null)
            {
                //throw new StatusCodeException(message: "Category not found", statusCode: StatusCodes.Status404NotFound);
            }
            _categoryRepository.Delete(request.Id);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
