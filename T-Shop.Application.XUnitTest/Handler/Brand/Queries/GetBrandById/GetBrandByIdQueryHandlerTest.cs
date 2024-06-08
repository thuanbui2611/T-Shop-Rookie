using AutoFixture;
using FluentAssertions;
using Moq;
using T_Shop.Application.Features.Brand.Queries.GetBrandById;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Brand.ResponseModel;

namespace T_Shop.Application.XUnitTest.Handler.Brand.Queries.GetBrandById;
public class GetBrandByIdQueryHandlerTest : TestSetup
{
    private readonly Mock<IBrandQueries> _mockBrandQueries;
    private readonly GetBrandByIdQueryHandler _handler;

    public GetBrandByIdQueryHandlerTest()
    {
        _mockBrandQueries = new Mock<IBrandQueries>();
        _handler = new GetBrandByIdQueryHandler(_mapperConfig, _mockBrandQueries.Object);
    }

    [Fact]
    public async Task Should_Return_BrandResponseModel_OnExistingBrandId()
    {
        // Arrange
        var expectedBrand = _fixture.Create<Domain.Entity.Brand>();
        var expectedResult = _mapperConfig.Map<BrandResponseModel>(expectedBrand);
        _mockBrandQueries.Setup(x => x.GetBrandByIdAsync(expectedBrand.Id)).ReturnsAsync(expectedBrand);

        var request = new GetBrandByIdQuery { ID = expectedBrand.Id };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _mockBrandQueries.Verify(x => x.GetBrandByIdAsync(expectedBrand.Id), Times.Once);
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_Throw_BadRequestException_OnNonExistingBrandId()
    {
        // Arrange
        var nonExistingBrandId = Guid.NewGuid();
        _mockBrandQueries.Setup(x => x.GetBrandByIdAsync(nonExistingBrandId)).ReturnsAsync((Domain.Entity.Brand)null);

        var request = new GetBrandByIdQuery { ID = nonExistingBrandId };

        // Act & Assert
        await FluentActions.Invoking(() => new GetBrandByIdQueryHandler(_mapperConfig, _mockBrandQueries.Object).Handle(request, CancellationToken.None))
            .Should().ThrowAsync<NotFoundException>()
            .WithMessage("Brand can not found");

        _mockBrandQueries.Verify(x => x.GetBrandByIdAsync(nonExistingBrandId), Times.Once);
    }
}
