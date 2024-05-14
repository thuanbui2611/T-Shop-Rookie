using MediatR;
using T_Shop.Application.Common.Exceptions;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.Features.Type.Commands.DeleteType;
public class DeleteTypeCommandHandler : IRequestHandler<DeleteTypeCommand, bool>
{
    private readonly IGenericRepository<TypeProduct> _typeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTypeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _typeRepository = _unitOfWork.GetBaseRepo<TypeProduct>();
    }

    public async Task<bool> Handle(DeleteTypeCommand request, CancellationToken cancellationToken)
    {
        var type = await _typeRepository.GetById(request.Id);
        if (type == null)
        {
            throw new NotFoundException("Type not found");
        }
        _typeRepository.Delete(request.Id);
        await _unitOfWork.CompleteAsync();
        return true;
    }
}
