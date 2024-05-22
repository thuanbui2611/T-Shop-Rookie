using AutoMapper;
using MediatR;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;

namespace T_Shop.Application.Features.ModelProduct.Queries.GetModelProductById;
public class GetModelByIdQueryHandler : IRequestHandler<GetModelByIdQuery, ModelProductResponseModel>
{
    private readonly IMapper _mapper;
    private readonly IModelQueries _modelQueries;

    public GetModelByIdQueryHandler(IMapper mapper, IModelQueries modelQueries)
    {
        _mapper = mapper;
        _modelQueries = modelQueries;
    }

    public async Task<ModelProductResponseModel> Handle(GetModelByIdQuery request, CancellationToken cancellationToken)
    {
        var model = await _modelQueries.GetModelByIdAsync(request.ID);
        if (model == null)
        {
            throw new NotFoundException("Model can not found");
        }
        var result = _mapper.Map<ModelProductResponseModel>(model);
        return result;
    }
}
