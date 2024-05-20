using AutoMapper;
using MediatR;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.SharedServices.StripeService;
using T_Shop.Shared.DTOs.Order.ResponseModel;

namespace T_Shop.Application.Features.Order.Commands;
public class CreateOrUpdateOrderCommandHandler : IRequestHandler<CreateOrUpdateOrderCommand, OrderResponseModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStripeService _stripeService;
    public CreateOrUpdateOrderCommandHandler()
    {

    }
    public Task<OrderResponseModel> Handle(CreateOrUpdateOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
