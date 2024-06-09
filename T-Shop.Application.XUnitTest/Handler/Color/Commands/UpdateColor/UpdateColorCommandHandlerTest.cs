using AutoFixture;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Features.Color.Commands.UpdateColor;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.XUnitTest.Handler.Color.Commands.UpdateColor;
public class UpdateColorCommandHandlerTest : TestSetup
{
    private readonly Mock<IColorQueries> _mockColorQueries;
    private readonly UpdateColorCommandHandler _handler;
    private readonly Mock<IGenericRepository<Domain.Entity.Color>> _colorRepositoryMock;

    public UpdateColorCommandHandlerTest()
    {
        _colorRepositoryMock = new Mock<IGenericRepository<Domain.Entity.Color>>();
        _mockUnitOfWork.Setup(uow => uow.GetBaseRepo<Domain.Entity.Color>()).Returns(_colorRepositoryMock.Object);
        _mockColorQueries = new Mock<IColorQueries>();
        _handler = new UpdateColorCommandHandler(_mockColorQueries.Object, _mockUnitOfWork.Object, _mapperConfig, _cacheMock.Object, _cacheKeyConstants);
    }

    [Fact]
    public async Task Should_UpdateColor_AndReturnMappedResponse_OnValidData()
    {
        // Arrange
        var request = _fixture.Create<UpdateColorCommand>();
        var existingColor = _fixture.Create<Domain.Entity.Color>();
        existingColor.Id = request.ID;
        _mockColorQueries.Setup(x => x.GetColorByIdAsync(request.ID)).ReturnsAsync(existingColor);
        _mockColorQueries.Setup(x => x.CheckIsColorExisted(request.Name)).ReturnsAsync(false);
        _mockUnitOfWork.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

        var cacheValues = _fixture.CreateMany<Domain.Entity.Color>().ToList();
        cacheValues.Add(existingColor);
        _cacheMock.Setup(c => c.GetAsync<List<Domain.Entity.Color>>(_cacheKeyConstants.ColorCacheKey)).ReturnsAsync(cacheValues);
        // Mocking DefaultCachePolicy and BuildOptions
        var cachePolicyMock = new Mock<CacheDefaults>();

        _cacheMock.Setup(c => c.DefaultCachePolicy).Returns(cachePolicyMock.Object);

        // Mock the Add method for IAppCache to avoid NullReferenceException
        _cacheMock.Setup(c => c.Add(It.IsAny<string>(), It.IsAny<List<Domain.Entity.Color>>(), It.IsAny<MemoryCacheEntryOptions>()))
                  .Verifiable();

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _mockColorQueries.Verify(x => x.GetColorByIdAsync(request.ID), Times.Once);
        _mockColorQueries.Verify(x => x.CheckIsColorExisted(request.Name), Times.Once);
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Once);
        _cacheMock.Verify(x => x.GetAsync<List<Domain.Entity.Color>>(_cacheKeyConstants.ColorCacheKey), Times.Once);
        _cacheMock.Verify(c => c.Add(_cacheKeyConstants.ColorCacheKey, It.IsAny<List<Domain.Entity.Color>>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Once);
        Assert.Equal(request.ID, result.ID);
    }

    [Fact]
    public async Task Should_ThrowNotFoundException_OnColorNotFound()
    {
        // Arrange
        var request = _fixture.Create<UpdateColorCommand>();
        _mockColorQueries.Setup(x => x.GetColorByIdAsync(request.ID)).ReturnsAsync((Domain.Entity.Color)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Never);
        _cacheMock.Verify(x => x.GetAsync<List<Domain.Entity.Color>>(_cacheKeyConstants.ColorCacheKey), Times.Never);
        _cacheMock.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Never);
    }

    [Fact]
    public async Task Should_ThrowConflictException_OnExistingColorName()
    {
        // Arrange
        var request = _fixture.Create<UpdateColorCommand>();
        var existingColor = _fixture.Build<Domain.Entity.Color>()
                            .Create();
        _mockColorQueries.Setup(x => x.GetColorByIdAsync(request.ID)).ReturnsAsync(existingColor);
        _mockColorQueries.Setup(x => x.CheckIsColorExisted(request.Name)).ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<ConflictException>(async () => await _handler.Handle(request, CancellationToken.None));
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Never);
        _cacheMock.Verify(x => x.GetAsync<List<Domain.Entity.Color>>(_cacheKeyConstants.ColorCacheKey), Times.Never);
        _cacheMock.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Never);
    }
}
