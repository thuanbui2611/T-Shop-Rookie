using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence.IdentityModels;
using T_Shop.Shared.DTOs.Transaction.ResponseModel;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.Application.Features.Transaction.Queries.GetTransactionById;
public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionWithCustomerResponseModel>
{
    private readonly IMapper _mapper;
    private readonly ITransactionQueries _transactionQueries;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetTransactionByIdQueryHandler(IMapper mapper, ITransactionQueries transactionQueries, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _transactionQueries = transactionQueries;
        _userManager = userManager;
    }

    public async Task<TransactionWithCustomerResponseModel> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionQueries.GetTransactionByIdAsync(request.TransactionId, false);

        if (transaction == null)
        {
            throw new BadRequestException(message: "Transaction not found");
        }
        ApplicationUser user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id.Equals(transaction.CustomerID));

        var result = _mapper.Map<TransactionWithCustomerResponseModel>(transaction);
        result.User = _mapper.Map<UserResponseModel>(user);
        return result;
    }
}
