using AutoFixture;
using FluentAssertions;
using Moq;
using T_Shop.Application.Features.ModelProduct.Queries.GetModelProductById;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;

namespace T_Shop.Application.XUnitTest.Handler.Model.GetModelsById;
public class GetModelByIdQueryHandlerTest : TestSetup
{
    private readonly Mock<IModelQueries> _mockModelQueries;
    private readonly GetModelByIdQueryHandler _handler;

    public GetModelByIdQueryHandlerTest()
    {
        _mockModelQueries = new Mock<IModelQueries>();
        _handler = new GetModelByIdQueryHandler(_mapperConfig, _mockModelQueries.Object);
    }

    [Fact]
    public async Task Should_Return_ModelProductResponseModel_OnExistingModelId()
    {
        // Arrange
        var expectedModel = _fixture.Create<Domain.Entity.Model>();
        var expectedResult = _mapperConfig.Map<ModelProductResponseModel>(expectedModel);
        _mockModelQueries.Setup(x => x.GetModelByIdAsync(expectedModel.Id)).ReturnsAsync(expectedModel);

        var request = new GetModelByIdQuery { ID = expectedModel.Id };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _mockModelQueries.Verify(x => x.GetModelByIdAsync(expectedModel.Id), Times.Once);
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_Throw_BadRequestException_OnNonExistingModelId()
    {
        // Arrange
        var nonExistingModelId = Guid.NewGuid();
        _mockModelQueries.Setup(x => x.GetModelByIdAsync(nonExistingModelId)).ReturnsAsync((Domain.Entity.Model)null);

        var request = new GetModelByIdQuery { ID = nonExistingModelId };

        // Act & Assert
        await FluentActions.Invoking(() => new GetModelByIdQueryHandler(_mapperConfig, _mockModelQueries.Object).Handle(request, CancellationToken.None))
            .Should().ThrowAsync<NotFoundException>()
            .WithMessage("Model can not found");

        _mockModelQueries.Verify(x => x.GetModelByIdAsync(nonExistingModelId), Times.Once);
    }
}
