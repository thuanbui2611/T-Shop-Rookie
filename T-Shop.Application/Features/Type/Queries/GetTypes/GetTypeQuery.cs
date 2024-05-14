using MediatR;
using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.Application.Features.Type.Queries.GetTypes;
public class GetTypeQuery : IRequest<List<TypeResponseModel>>
{
}
