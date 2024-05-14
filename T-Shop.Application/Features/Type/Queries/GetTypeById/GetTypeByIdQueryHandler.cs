using AutoMapper;
using MediatR;
using T_Shop.Application.Common.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.Application.Features.Type.Queries.GetTypeById;
public class GetTypeByIdQueryHandler : IRequestHandler<GetTypeByIdQuery, TypeResponseModel>
{
    private readonly IMapper _mapper;
    private readonly ITypeQueries _typeQueries;

    public GetTypeByIdQueryHandler(IMapper mapper, ITypeQueries typeQueries)
    {
        _mapper = mapper;
        _typeQueries = typeQueries;
    }

    public async Task<TypeResponseModel> Handle(GetTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var type = await _typeQueries.GetTypeByIdAsync(request.Id);
        if (type == null)
        {
            throw new NotFoundException("Type can not found");
        }
        var result = _mapper.Map<TypeResponseModel>(type);
        return result;
    }
}
