using AutoMapper;
using MediatR;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Color.ResponseModel;

namespace T_Shop.Application.Features.Color.Commands.CreateColor;
public class CreateColorCommandHandler : IRequestHandler<CreateColorCommand, ColorResponseModel>
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Entity.Color> _colorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IColorQueries _colorQueries;

    public CreateColorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IColorQueries colorQueries)
    {
        _mapper = mapper;
        _colorRepository = unitOfWork.GetBaseRepo<Domain.Entity.Color>();
        _unitOfWork = unitOfWork;
        _colorQueries = colorQueries;
    }

    public async Task<ColorResponseModel> Handle(CreateColorCommand request, CancellationToken cancellationToken)
    {
        if (await _colorQueries.CheckIsColorExisted(request.Name)) throw new ConflictException("The color is existed");

        var newType = _mapper.Map<Domain.Entity.Color>(request);
        var colorAdded = _colorRepository.Add(newType);
        await _unitOfWork.CompleteAsync();

        var result = _mapper.Map<ColorResponseModel>(colorAdded);
        return result;
    }
}
