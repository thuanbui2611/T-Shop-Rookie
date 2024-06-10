using AutoFixture;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Features.ModelProduct.Commands.DeleteModelProduct;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.XUnitTest.Handler.Model.Commands.DeleteModel;
public class DeleteModelCommandHandlerTest : TestSetup
{
    private readonly Mock<IGenericRepository<Domain.Entity.Model>> _colorRepositoryMock;
    private readonly DeleteModelProductCommandHandler _handler;

    public DeleteModelCommandHandlerTest()
    {
        _colorRepositoryMock = new Mock<IGenericRepository<Domain.Entity.Model>>();
        _mockUnitOfWork.Setup(uow => uow.GetBaseRepo<Domain.Entity.Model>()).Returns(_colorRepositoryMock.Object);

        _handler = new DeleteModelProductCommandHandler(_mockUnitOfWork.Object, _cacheMock.Object, _cacheKeyConstants);
        _mockUnitOfWork.Setup(uow => uow.GetBaseRepo<Domain.Entity.Model>()).Returns(_colorRepositoryMock.Object);
    }
    [Fact]
    public async Task Should_DeleteModel_UpdateCache_AndReturnTrue_OnExistingModel()
    {
        // Arrange
        var request = _fixture.Create<DeleteModelProductCommand>();
        var existingModel = _fixture.Build<Domain.Entity.Model>()
            .With(b => b.Id, request.ID)
            .Create();
        _colorRepositoryMock.Setup(x => x.GetById(request.ID)).ReturnsAsync(existingModel);
        _mockUnitOfWork.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);
        var cacheValues = _fixture.CreateMany<Domain.Entity.Model>().ToList();
        _cacheMock.Setup(c => c.GetAsync<List<Domain.Entity.Model>>(_cacheKeyConstants.ModelCacheKey)).ReturnsAsync(cacheValues);

        // Mocking DefaultCachePolicy and BuildOptions
        var cachePolicyMock = new Mock<CacheDefaults>();

        _cacheMock.Setup(c => c.DefaultCachePolicy).Returns(cachePolicyMock.Object);

        // Mock the Add method for IAppCache to avoid NullReferenceException
        _cacheMock.Setup(c => c.Add(_cacheKeyConstants.ModelCacheKey, It.IsAny<List<Domain.Entity.Model>>(), It.IsAny<MemoryCacheEntryOptions>()))
                  .Verifiable();
        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _colorRepositoryMock.Verify(x => x.GetById(request.ID), Times.Once);
        _colorRepositoryMock.Verify(x => x.Delete(request.ID), Times.Once);
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Once);
        _cacheMock.Verify(x => x.GetAsync<List<Domain.Entity.Model>>(_cacheKeyConstants.ModelCacheKey), Times.Once);
        _cacheMock.Verify(x => x.Add(_cacheKeyConstants.ModelCacheKey, It.IsAny<List<Domain.Entity.Model>>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Once);
        Assert.True(result);
    }

    [Fact]
    public async Task Should_ThrowNotFoundException_OnNonexistentModel()
    {
        // Arrange
        var request = _fixture.Create<DeleteModelProductCommand>();
        _colorRepositoryMock.Setup(x => x.GetById(request.ID)).ReturnsAsync((Domain.Entity.Model)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));
        _colorRepositoryMock.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Never);
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Never);
        _cacheMock.Verify(x => x.GetAsync<List<Domain.Entity.Model>>(_cacheKeyConstants.ModelCacheKey), Times.Never);
        _cacheMock.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Never);
    }
}
