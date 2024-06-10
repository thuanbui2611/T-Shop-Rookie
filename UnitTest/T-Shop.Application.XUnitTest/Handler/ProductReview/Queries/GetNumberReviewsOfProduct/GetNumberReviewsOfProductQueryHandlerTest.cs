using AutoFixture;
using Moq;
using T_Shop.Application.Features.ProductReview.Queries.GetNumberReviewsOfProduct;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.ProductReview.ResponseModel;

namespace T_Shop.Application.XUnitTest.Handler.ProductReview.Queries.GetNumberReviewsOfProduct;
public class GetNumberReviewsOfProductQueryHandlerTest : TestSetup
{
    private readonly Mock<IProductReviewQueries> _mockProductReviewQueries;
    private readonly GetNumberReviewsOfProductQueryHandler _handler;

    public GetNumberReviewsOfProductQueryHandlerTest()
    {
        _mockProductReviewQueries = new Mock<IProductReviewQueries>();
        _handler = new GetNumberReviewsOfProductQueryHandler(_mapperConfig, _mockProductReviewQueries.Object);
    }

    [Fact]
    public async Task Should_Return_ProductReviewCount_WithCorrectValues()
    {
        // Arrange
        var expectedProductReviews = _fixture.CreateMany<Domain.Entity.ProductReview>().ToList();
        var expectedNumberOfReviews = expectedProductReviews.Count();
        var productId = Guid.NewGuid();
        var request = new GetNumberReviewsOfProductQuery { ProductID = productId };
        var expectedResult = new ProductReviewCountResponseModel { ProductID = productId, TotalReviews = expectedNumberOfReviews };

        _mockProductReviewQueries.Setup(x => x.CountTotalReviewByProductId(productId))
            .ReturnsAsync(expectedNumberOfReviews);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _mockProductReviewQueries.Verify(x => x.CountTotalReviewByProductId(productId), Times.Once);
        Assert.Equal(expectedResult.ProductID, result.ProductID);
        Assert.Equal(expectedResult.TotalReviews, result.TotalReviews);
    }
}
