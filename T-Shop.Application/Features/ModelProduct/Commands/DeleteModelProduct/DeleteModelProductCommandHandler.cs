using MediatR;
using T_Shop.Application.Common.Exceptions;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.Features.ModelProduct.Commands.DeleteModelProduct;
public class DeleteModelProductCommandHandler : IRequestHandler<DeleteModelProductCommand, bool>
{
    private readonly IGenericRepository<Model> _modelRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteModelProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _modelRepository = _unitOfWork.GetBaseRepo<Model>();
    }

    public async Task<bool> Handle(DeleteModelProductCommand request, CancellationToken cancellationToken)
    {
        var model = await _modelRepository.GetById(request.ID);
        if (model == null)
        {
            throw new NotFoundException("Model not found");
        }
        _modelRepository.Delete(request.ID);
        await _unitOfWork.CompleteAsync();
        return true;
    }
}
