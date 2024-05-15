using AutoMapper;
using MediatR;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.Application.Features.Type.Queries.GetTypes;
public class GetTypesQueryHandler : IRequestHandler<GetTypesQuery, List<TypeResponseModel>>
{
    private readonly IMapper _mapper;
    private readonly ITypeQueries _typeQueries;

    public GetTypesQueryHandler(IMapper mapper, ITypeQueries typeQueries)
    {
        _mapper = mapper;
        _typeQueries = typeQueries;
    }

    public async Task<List<TypeResponseModel>> Handle(GetTypesQuery request, CancellationToken cancellationToken)
    {
        var types = await _typeQueries.GetTypesAsync();
        var result = _mapper.Map<List<TypeResponseModel>>(types);
        return result;
    }
}
