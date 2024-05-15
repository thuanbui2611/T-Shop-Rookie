using MediatR;
using T_Shop.Application.Common.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.Features.Color.Commands.DeleteColor;
public class ColorDeleteCommandHandler : IRequestHandler<ColorDeleteCommand, bool>
{
    private readonly IGenericRepository<Domain.Entity.Color> _colorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ColorDeleteCommandHandler(IUnitOfWork unitOfWork)
    {
        _colorRepository = _unitOfWork.GetBaseRepo<Domain.Entity.Color>();
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(ColorDeleteCommand request, CancellationToken cancellationToken)
    {
        var color = await _colorRepository.GetById(request.ID);
        if (color == null)
        {
            throw new NotFoundException("Color not found");
        }
        _colorRepository.Delete(request.ID);
        await _unitOfWork.CompleteAsync();
        return true;
    }
}
