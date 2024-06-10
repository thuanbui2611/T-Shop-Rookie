using AutoFixture;
using FluentAssertions;
using Moq;
using T_Shop.Application.Features.Transaction.Queries.GetTransactions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Pagination;
using T_Shop.Shared.DTOs.Transaction.ResponseModel;

namespace T_Shop.Application.XUnitTest.Handler.Transaction.Queries.GetTransactions;
public class GetTransactionsQueryHandlerTest : TestSetup
{
    private readonly Mock<ITransactionQueries> _mockTransactionQueries;
    private readonly GetTransactionsQueryHandler _handler;

    public GetTransactionsQueryHandlerTest()
    {
        _mockTransactionQueries = new Mock<ITransactionQueries>();
        _handler = new GetTransactionsQueryHandler(_mapperConfig, _mockTransactionQueries.Object);
    }

    [Fact]
    public async Task Should_Return_MappedTransactionsAndPagination()
    {
        // Arrange
        var query = _fixture.Build<GetTransactionsQuery>().Create();
        var expectedTransactions = _fixture.CreateMany<Domain.Entity.Transaction>().ToList();
        var expectedPagination = new PaginationMetaData(expectedTransactions.Count, query.Pagination.pageSize, query.Pagination.pageNumber);
        var expectedResult = _mapperConfig.Map<List<TransactionResponseModel>>(expectedTransactions);

        _mockTransactionQueries.Setup(x => x.GetTransactionsAsync(query.Pagination))
            .ReturnsAsync((expectedTransactions, expectedPagination));

        // Act
        var (transactionsResult, paginationResult) = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _mockTransactionQueries.Verify(x => x.GetTransactionsAsync(query.Pagination), Times.Once);

        transactionsResult.Should().BeEquivalentTo(expectedResult);
        paginationResult.Should().BeEquivalentTo(expectedPagination);
    }
}
