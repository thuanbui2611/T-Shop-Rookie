using AutoMapper;
using MediatR;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Color.ResponseModel;

namespace T_Shop.Application.Features.Color.Commands.UpdateColor;
public class UpdateColorCommandHandler : IRequestHandler<UpdateColorCommand, ColorResponseModel>
{
    private readonly IGenericRepository<Domain.Entity.Color> _colorRepository;
    private readonly IColorQueries _colorQueries;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateColorCommandHandler(IColorQueries colorQueries, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _colorRepository = unitOfWork.GetBaseRepo<Domain.Entity.Color>();
        _colorQueries = colorQueries;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ColorResponseModel> Handle(UpdateColorCommand request, CancellationToken cancellationToken)
    {
        var color = await _colorQueries.GetColorByIdAsync(request.ID);
        if (color == null)
        {
            throw new NotFoundException("Color not found");
        }
        var isColorExisted = await _colorQueries.CheckIsColorExisted(request.Name);

        if (isColorExisted && (color.Name.ToLower() != request.Name.ToLower()))
        {
            throw new ConflictException("Color is existed");
        }
        var colorUpdated = _mapper.Map<Domain.Entity.Color>(request);
        _colorRepository.Update(colorUpdated);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<ColorResponseModel>(colorUpdated);
    }
}
