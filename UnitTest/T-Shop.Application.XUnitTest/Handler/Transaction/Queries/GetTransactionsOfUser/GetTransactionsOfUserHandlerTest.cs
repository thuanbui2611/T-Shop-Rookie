using AutoFixture;
using FluentAssertions;
using Moq;
using T_Shop.Application.Features.Transaction.Queries.GetTransactionsOfUser;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Pagination;
using T_Shop.Shared.DTOs.Transaction.ResponseModel;

namespace T_Shop.Application.XUnitTest.Handler.Transaction.Queries.GetTransactionsOfUser;
public class GetTransactionsOfUserHandlerTest : TestSetup
{
    private readonly Mock<ITransactionQueries> _mockTransactionQueries;
    private readonly GetTransactionsOfUserQueryHandler _handler;

    public GetTransactionsOfUserHandlerTest()
    {
        _mockTransactionQueries = new Mock<ITransactionQueries>();
        _handler = new GetTransactionsOfUserQueryHandler(_mapperConfig, _mockTransactionQueries.Object);
    }

    [Fact]
    public async Task Should_Return_MappedTransactionsAndPagination_ForExistingUser()
    {
        // Arrange
        var query = _fixture.Build<GetTransactionsOfUserQuery>().Create();
        var expectedTransactions = _fixture.CreateMany<Domain.Entity.Transaction>().ToList();
        var expectedPagination = new PaginationMetaData(expectedTransactions.Count, query.Pagination.pageSize, query.Pagination.pageNumber);
        var expectedResult = _mapperConfig.Map<List<TransactionResponseModel>>(expectedTransactions);
        //var userId = Guid.NewGuid();

        _mockTransactionQueries.Setup(x => x.GetTransactionsByUserIdAsync(query.Pagination, query.UserId))
            .ReturnsAsync((expectedTransactions, expectedPagination));

        // Act
        var (transactionsResult, paginationResult) = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _mockTransactionQueries.Verify(x => x.GetTransactionsByUserIdAsync(query.Pagination, query.UserId), Times.Once);
        transactionsResult.Should().BeEquivalentTo(expectedResult);
        paginationResult.Should().BeEquivalentTo(expectedPagination);
    }

    [Fact]
    public async Task Should_Return_EmptyListAndPagination_ForNonExistingUser()
    {
        // Arrange
        var query = _fixture.Build<GetTransactionsOfUserQuery>().Create();
        var expectedPagination = new PaginationMetaData(0, query.Pagination.pageNumber, query.Pagination.pageSize);

        _mockTransactionQueries.Setup(x => x.GetTransactionsByUserIdAsync(query.Pagination, It.IsAny<Guid>()))
            .ReturnsAsync((new List<Domain.Entity.Transaction>(), expectedPagination));

        // Act
        var (transactionsResult, paginationResult) = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _mockTransactionQueries.Verify(x => x.GetTransactionsByUserIdAsync(query.Pagination, It.IsAny<Guid>()), Times.Once);
        transactionsResult.Should().BeNullOrEmpty();
        expectedPagination.Should().BeEquivalentTo(paginationResult);
    }


}
