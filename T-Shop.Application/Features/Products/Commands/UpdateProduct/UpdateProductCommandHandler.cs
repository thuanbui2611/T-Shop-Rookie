using AutoMapper;
using MediatR;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Entity.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Product;

namespace T_Shop.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
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

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryQueries.CheckCategoryExisted(request.CategoryId);
            if (!category)
            {
                throw new BadRequestException(message: "Category not found");
            }
            var product = await _productRepository.GetById(request.Id);
            if (product == null)
            {
                throw new BadRequestException(message: "Product not found");
            }
            var productUpdate = _mapper.Map<Product>(request);
            _productRepository.Update(productUpdate);
            await _unitOfWork.CompleteAsync();
            var result = _mapper.Map<ProductDto>(productUpdate);
            return result;
        }
    }
}
