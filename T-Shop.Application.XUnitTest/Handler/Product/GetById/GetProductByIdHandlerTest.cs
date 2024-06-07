using AutoFixture;
using FluentAssertions;
using Moq;
using T_Shop.Application.Features.Products.Queries.GetProductsById;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Product.ResponseModel;

namespace T_Shop.Application.XUnitTest.Handler.Products.GetById;
public class GetProductByIdHandlerTest : TestSetup
{
    private readonly Mock<IProductQueries> _mockProductQueries;
    private readonly GetProductByIdQueryHandler _handler;

    public GetProductByIdHandlerTest()
    {
        _mockProductQueries = new Mock<IProductQueries>();
        _handler = new GetProductByIdQueryHandler(_mapperConfig, _mockProductQueries.Object);
    }

    [Fact]
    public async Task Should_Return_ProductResponseModel_OnExistingProductId()
    {
        // Arrange
        var expectedProduct = _fixture.Create<Product>();
        var expectedResult = _mapperConfig.Map<ProductResponseModel>(expectedProduct);
        _mockProductQueries.Setup(x => x.GetProductByIdAsync(expectedProduct.Id)).ReturnsAsync(expectedProduct);

        var request = new GetProductByIdQuery { productId = expectedProduct.Id };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _mockProductQueries.Verify(x => x.GetProductByIdAsync(expectedProduct.Id), Times.Once);
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_Throw_BadRequestException_OnNonExistingProductId()
    {
        // Arrange
        var nonExistingProductId = Guid.NewGuid();
        _mockProductQueries.Setup(x => x.GetProductByIdAsync(nonExistingProductId)).ReturnsAsync((Product)null);

        var request = new GetProductByIdQuery { productId = nonExistingProductId };

        // Act & Assert
        await FluentActions.Invoking(() => new GetProductByIdQueryHandler(_mapperConfig, _mockProductQueries.Object).Handle(request, CancellationToken.None))
            .Should().ThrowAsync<NotFoundException>()
            .WithMessage("Product not found");

        _mockProductQueries.Verify(x => x.GetProductByIdAsync(nonExistingProductId), Times.Once);
    }
}
