using AutoFixture;
using FluentAssertions;
using Moq;
using T_Shop.Application.Features.Type.Queries.GetTypeById;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.Application.XUnitTest.Handler.Type.GetTypeById;
public class GetTypeByIdQueryHandlerTest : TestSetup
{
    private readonly Mock<ITypeQueries> _mockTypeQueries;
    private readonly GetTypeByIdQueryHandler _handler;

    public GetTypeByIdQueryHandlerTest()
    {
        _mockTypeQueries = new Mock<ITypeQueries>();
        _handler = new GetTypeByIdQueryHandler(_mapperConfig, _mockTypeQueries.Object);
    }

    [Fact]
    public async Task Should_Return_TypeResponseModel_OnExistingTypeId()
    {
        // Arrange
        var expectedType = _fixture.Create<Domain.Entity.TypeProduct>();
        var expectedResult = _mapperConfig.Map<TypeResponseModel>(expectedType);
        _mockTypeQueries.Setup(x => x.GetTypeByIdAsync(expectedType.Id)).ReturnsAsync(expectedType);

        var request = new GetTypeByIdQuery { Id = expectedType.Id };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _mockTypeQueries.Verify(x => x.GetTypeByIdAsync(expectedType.Id), Times.Once);
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_Throw_BadRequestException_OnNonExistingTypeId()
    {
        // Arrange
        var nonExistingTypeId = Guid.NewGuid();
        _mockTypeQueries.Setup(x => x.GetTypeByIdAsync(nonExistingTypeId)).ReturnsAsync((Domain.Entity.TypeProduct)null);

        var request = new GetTypeByIdQuery { Id = nonExistingTypeId };

        // Act & Assert
        await FluentActions.Invoking(() => new GetTypeByIdQueryHandler(_mapperConfig, _mockTypeQueries.Object).Handle(request, CancellationToken.None))
            .Should().ThrowAsync<NotFoundException>()
            .WithMessage("Type can not found");

        _mockTypeQueries.Verify(x => x.GetTypeByIdAsync(nonExistingTypeId), Times.Once);
    }
}
