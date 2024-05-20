using MediatR;
using T_Shop.Domain.Exceptions;
using T_Shop.Application.Common.Helpers;
using T_Shop.Application.Common.ServiceInterface;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductImage> _imageProductRepository;
        private readonly IProductQueries _productQueries;
        private readonly ICloudinaryService _cloudinaryService;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IProductQueries productQueries, ICloudinaryService cloudinaryService)
        {
            _productRepository = unitOfWork.GetBaseRepo<Product>();
            _imageProductRepository = unitOfWork.GetBaseRepo<ProductImage>();
            _unitOfWork = unitOfWork;
            _productQueries = productQueries;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productQueries.GetByIdAsync(request.Id);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }

            //Delete product image
            foreach (var image in product.ProductImages)
            {
                await _cloudinaryService.DeleteImageAsync(ImageHelpers.GetPublicIDFromImageUrl(image.ImageUrl));
            }
            _imageProductRepository.DeleteRange(product.ProductImages);
            _productRepository.Delete(request.Id);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
