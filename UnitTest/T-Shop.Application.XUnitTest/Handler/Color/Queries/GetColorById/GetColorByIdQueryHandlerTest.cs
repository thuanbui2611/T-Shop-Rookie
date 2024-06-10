using AutoFixture;
using FluentAssertions;
using Moq;
using T_Shop.Application.Features.Color.Queries.GetColorById;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Color.ResponseModel;

namespace T_Shop.Application.XUnitTest.Handler.Color.Queries.GetColorById;
public class GetColorByIdQueryHandlerTest : TestSetup
{
    private readonly Mock<IColorQueries> _mockColorQueries;
    private readonly GetColorByIdQueryHandler _handler;

    public GetColorByIdQueryHandlerTest()
    {
        _mockColorQueries = new Mock<IColorQueries>();
        _handler = new GetColorByIdQueryHandler(_mapperConfig, _mockColorQueries.Object);
    }

    [Fact]
    public async Task Should_Return_ColorResponseModel_OnExistingColorId()
    {
        // Arrange
        var expectedColor = _fixture.Create<Domain.Entity.Color>();
        var expectedResult = _mapperConfig.Map<ColorResponseModel>(expectedColor);
        _mockColorQueries.Setup(x => x.GetColorByIdAsync(expectedColor.Id)).ReturnsAsync(expectedColor);

        var request = new GetColorByIdQuery { ID = expectedColor.Id };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _mockColorQueries.Verify(x => x.GetColorByIdAsync(expectedColor.Id), Times.Once);
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_Throw_BadRequestException_OnNonExistingColorId()
    {
        // Arrange
        var nonExistingColorId = Guid.NewGuid();
        _mockColorQueries.Setup(x => x.GetColorByIdAsync(nonExistingColorId)).ReturnsAsync((Domain.Entity.Color)null);

        var request = new GetColorByIdQuery { ID = nonExistingColorId };

        // Act & Assert
        await FluentActions.Invoking(() => new GetColorByIdQueryHandler(_mapperConfig, _mockColorQueries.Object).Handle(request, CancellationToken.None))
            .Should().ThrowAsync<NotFoundException>()
            .WithMessage("Color can not found");

        _mockColorQueries.Verify(x => x.GetColorByIdAsync(nonExistingColorId), Times.Once);
    }
}
