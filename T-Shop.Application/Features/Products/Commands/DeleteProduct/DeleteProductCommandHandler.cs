using MediatR;
using T_Shop.Application.Common.Exceptions;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _productRepository = unitOfWork.GetBaseRepo<Product>();
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = _productRepository.GetById(request.Id);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }
            _productRepository.Delete(request.Id);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
