using AutoMapper;
using MediatR;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Product;

namespace T_Shop.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Category> _categoryRepository;

        public CreateProductCommandHandler(IMapper mapper, IGenericRepository<Product> productRepository, IUnitOfWork unitOfWork, IGenericRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //Will use validator
            if (string.IsNullOrEmpty(request.Name))
            {
                //throw new StatusCodeException(message: "Product name is required.", statusCode: StatusCodes.Status400BadRequest);
            }
            var category = _categoryRepository.GetById(request.CategoryId);
            if (category == null)
            {
                //throw new StatusCodeException(message: "Category is not existed.", statusCode: StatusCodes.Status400BadRequest);
            }
            var newProduct = _mapper.Map<Product>(request);
            _productRepository.Add(newProduct);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<ProductDto>(newProduct);
            return result;
        }
    }
}
