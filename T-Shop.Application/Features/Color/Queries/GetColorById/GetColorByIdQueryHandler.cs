using AutoMapper;
using MediatR;
using T_Shop.Application.Common.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Color.ResponseModel;

namespace T_Shop.Application.Features.Color.Queries.GetColorById;
public class GetColorByIdQueryHandler : IRequestHandler<GetColorByIdQuery, ColorResponseModel>
{
    private readonly IMapper _mapper;
    private readonly IColorQueries _colorQueries;

    public GetColorByIdQueryHandler(IMapper mapper, IColorQueries colorQueries)
    {
        _mapper = mapper;
        _colorQueries = colorQueries;
    }

    public async Task<ColorResponseModel> Handle(GetColorByIdQuery request, CancellationToken cancellationToken)
    {
        var color = await _colorQueries.GetColorByIdAsync(request.ID);
        if (color == null)
        {
            throw new NotFoundException("Color can not found");
        }
        var result = _mapper.Map<ColorResponseModel>(color);
        return result;
    }
}
