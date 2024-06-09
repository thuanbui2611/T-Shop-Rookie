using AutoFixture;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Features.Color.Commands.DeleteColor;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.XUnitTest.Handler.Color.Commands.DeleteColor;
public class DeleteColorCommandHandlerTest : TestSetup
{
    private readonly Mock<IGenericRepository<Domain.Entity.Color>> _colorRepositoryMock;
    private readonly ColorDeleteCommandHandler _handler;

    public DeleteColorCommandHandlerTest()
    {
        _colorRepositoryMock = new Mock<IGenericRepository<Domain.Entity.Color>>();
        _mockUnitOfWork.Setup(uow => uow.GetBaseRepo<Domain.Entity.Color>()).Returns(_colorRepositoryMock.Object);

        _handler = new ColorDeleteCommandHandler(_mockUnitOfWork.Object, _cacheMock.Object, _cacheKeyConstants);
        _mockUnitOfWork.Setup(uow => uow.GetBaseRepo<Domain.Entity.Color>()).Returns(_colorRepositoryMock.Object);
    }
    [Fact]
    public async Task Should_DeleteColor_UpdateCache_AndReturnTrue_OnExistingColor()
    {
        // Arrange
        var request = _fixture.Create<ColorDeleteCommand>();
        var existingColor = _fixture.Build<Domain.Entity.Color>()
            .With(b => b.Id, request.ID)
            .Create();
        _colorRepositoryMock.Setup(x => x.GetById(request.ID)).ReturnsAsync(existingColor);
        _mockUnitOfWork.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);
        var cacheValues = _fixture.CreateMany<Domain.Entity.Color>().ToList();
        _cacheMock.Setup(c => c.GetAsync<List<Domain.Entity.Color>>(_cacheKeyConstants.ColorCacheKey)).ReturnsAsync(cacheValues);

        // Mocking DefaultCachePolicy and BuildOptions
        var cachePolicyMock = new Mock<CacheDefaults>();

        _cacheMock.Setup(c => c.DefaultCachePolicy).Returns(cachePolicyMock.Object);

        // Mock the Add method for IAppCache to avoid NullReferenceException
        _cacheMock.Setup(c => c.Add(_cacheKeyConstants.ColorCacheKey, It.IsAny<List<Domain.Entity.Color>>(), It.IsAny<MemoryCacheEntryOptions>()))
                  .Verifiable();
        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _colorRepositoryMock.Verify(x => x.GetById(request.ID), Times.Once);
        _colorRepositoryMock.Verify(x => x.Delete(request.ID), Times.Once);
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Once);
        _cacheMock.Verify(x => x.GetAsync<List<Domain.Entity.Color>>(_cacheKeyConstants.ColorCacheKey), Times.Once);
        _cacheMock.Verify(x => x.Add(_cacheKeyConstants.ColorCacheKey, It.IsAny<List<Domain.Entity.Color>>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Once);
        Assert.True(result);
    }

    [Fact]
    public async Task Should_ThrowNotFoundException_OnNonexistentColor()
    {
        // Arrange
        var request = _fixture.Create<ColorDeleteCommand>();
        _colorRepositoryMock.Setup(x => x.GetById(request.ID)).ReturnsAsync((Domain.Entity.Color)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));
        _colorRepositoryMock.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Never);
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Never);
        _cacheMock.Verify(x => x.GetAsync<List<Domain.Entity.Color>>(_cacheKeyConstants.ColorCacheKey), Times.Never);
        _cacheMock.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Never);
    }
}
