using AutoMapper;
using MediatR;
using T_Shop.Application.Features.Products.ViewModels;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDtos>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly ICategoryQueries _categoryQueries;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IMapper mapper, IGenericRepository<Product> productRepository, IUnitOfWork unitOfWork, ICategoryQueries categoryQueries)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _categoryQueries = categoryQueries;
        }

        public async Task<ProductDtos> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryQueries.CheckCategoryExisted(request.CategoryId);
            if (!category)
            {
                //throw new StatusCodeException(message: "Category not found", statusCode: StatusCodes.Status404NotFound);
            }
            var product = await _productRepository.GetById(request.Id);
            if (product == null)
            {
                //throw new StatusCodeException(message: "Product not found", statusCode: StatusCodes.Status404NotFound);
            }
            var productUpdate = _mapper.Map<Product>(request);
            _productRepository.Update(productUpdate);
            await _unitOfWork.CompleteAsync();
            var result = _mapper.Map<ProductDtos>(productUpdate);
            return result;
        }
    }
}
