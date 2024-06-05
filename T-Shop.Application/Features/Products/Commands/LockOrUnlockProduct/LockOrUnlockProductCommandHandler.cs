using MediatR;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.Features.Products.Commands.UpdateProductStatus;
public class LockOrUnlockProductCommandHandler : IRequestHandler<LockOrUnlockProductCommand, bool>
{
    private readonly IGenericRepository<Domain.Entity.Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LockOrUnlockProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _productRepository = unitOfWork.GetBaseRepo<Domain.Entity.Product>();
    }

    public async Task<bool> Handle(LockOrUnlockProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.ProductID);
        if (product == null) throw new NotFoundException("Product not found!");

        product.IsOnStock = !product.IsOnStock;
        _productRepository.Update(product);
        await _unitOfWork.CompleteAsync();
        return true;
    }
}
