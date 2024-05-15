using MediatR;
using T_Shop.Application.Common.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.Features.Brand.Command.DeleteBrand;
public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, bool>
{
    private readonly IGenericRepository<Domain.Entity.Brand> _brandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBrandCommandHandler(IUnitOfWork unitOfWork)
    {
        _brandRepository = unitOfWork.GetBaseRepo<Domain.Entity.Brand>();
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var type = await _brandRepository.GetById(request.ID);
        if (type == null)
        {
            throw new NotFoundException("Brand not found");
        }
        _brandRepository.Delete(request.ID);
        await _unitOfWork.CompleteAsync();
        return true;
    }
}
