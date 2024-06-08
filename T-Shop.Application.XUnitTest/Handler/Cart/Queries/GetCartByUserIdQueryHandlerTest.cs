using AutoFixture;
using FluentAssertions;
using Moq;
using T_Shop.Application.Features.Cart.Queries.GetCartByUserId;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Cart.ResponseModel;

namespace T_Shop.Application.XUnitTest.Handler.Cart.Queries;
public class GetCartByUserIdQueryHandlerTest : TestSetup
{
    private readonly Mock<ICartQueries> _mockCartQueries;
    private readonly GetCartByUserIdQueryHandler _handler;

    public GetCartByUserIdQueryHandlerTest()
    {
        _mockCartQueries = new Mock<ICartQueries>();
        _handler = new GetCartByUserIdQueryHandler(_mapperConfig, _mockCartQueries.Object);
    }

    [Fact]
    public async Task Should_Return_MappedCart_OnExistingCart()
    {
        // Arrange
        var expectedCart = _fixture.Create<Domain.Entity.Cart>();
        var expectedResult = _mapperConfig.Map<CartResponseModel>(expectedCart);

        _mockCartQueries.Setup(x => x.GetCartByUserIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(expectedCart);

        var request = new GetCartByUserIdQuery { UserID = Guid.NewGuid() };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _mockCartQueries.Verify(x => x.GetCartByUserIdAsync(request.UserID), Times.Once);
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_Return_Null_OnNonExistingCart()
    {
        // Arrange
        _mockCartQueries.Setup(x => x.GetCartByUserIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Domain.Entity.Cart)null);

        var request = new GetCartByUserIdQuery { UserID = Guid.NewGuid() };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _mockCartQueries.Verify(x => x.GetCartByUserIdAsync(request.UserID), Times.Once);
        Assert.Null(result);
    }
}
