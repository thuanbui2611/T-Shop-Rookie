using AutoMapper;
using MediatR;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Color.ResponseModel;

namespace T_Shop.Application.Features.Color.Queries.GetColors;
public class GetColorsQueryHandler : IRequestHandler<GetColorsQuery, List<ColorResponseModel>>
{
    private readonly IMapper _mapper;
    private readonly IColorQueries _colorQueries;

    public GetColorsQueryHandler(IMapper mapper, IColorQueries colorQueries)
    {
        _mapper = mapper;
        _colorQueries = colorQueries;
    }

    public async Task<List<ColorResponseModel>> Handle(GetColorsQuery request, CancellationToken cancellationToken)
    {
        var colors = await _colorQueries.GetColorsAsync();
        var result = _mapper.Map<List<ColorResponseModel>>(colors);
        return result;
    }
}
