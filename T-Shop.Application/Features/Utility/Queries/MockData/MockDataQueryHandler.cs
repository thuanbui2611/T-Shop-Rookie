using MediatR;

namespace T_Shop.Application.Features.Utility.Queries.MockData;
public class MockDataQueryHandler : IRequestHandler<MockDataQuery, bool>
{
    public Task<bool> Handle(MockDataQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
