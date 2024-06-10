using AutoFixture;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Features.Type.Commands.DeleteType;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.XUnitTest.Handler.Type.Commands.DeleteType;
public class DeleteTypeCommandHandlerTest : TestSetup
{
    private readonly Mock<IGenericRepository<Domain.Entity.TypeProduct>> _typeRepositoryMock;
    private readonly DeleteTypeCommandHandler _handler;

    public DeleteTypeCommandHandlerTest()
    {
        _typeRepositoryMock = new Mock<IGenericRepository<Domain.Entity.TypeProduct>>();
        _mockUnitOfWork.Setup(uow => uow.GetBaseRepo<Domain.Entity.TypeProduct>()).Returns(_typeRepositoryMock.Object);

        _handler = new DeleteTypeCommandHandler(_mockUnitOfWork.Object, _cacheMock.Object, _cacheKeyConstants);
        _mockUnitOfWork.Setup(uow => uow.GetBaseRepo<Domain.Entity.TypeProduct>()).Returns(_typeRepositoryMock.Object);
    }
    [Fact]
    public async Task Should_DeleteType_UpdateCache_AndReturnTrue_OnExistingType()
    {
        // Arrange
        var request = _fixture.Create<DeleteTypeCommand>();
        var existingType = _fixture.Build<Domain.Entity.TypeProduct>()
            .With(b => b.Id, request.Id)
            .Create();
        _typeRepositoryMock.Setup(x => x.GetById(request.Id)).ReturnsAsync(existingType);
        _mockUnitOfWork.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);
        var cacheValues = _fixture.CreateMany<Domain.Entity.TypeProduct>().ToList();
        _cacheMock.Setup(c => c.GetAsync<List<Domain.Entity.TypeProduct>>(_cacheKeyConstants.TypeCacheKey)).ReturnsAsync(cacheValues);

        // Mocking DefaultCachePolicy and BuildOptions
        var cachePolicyMock = new Mock<CacheDefaults>();

        _cacheMock.Setup(c => c.DefaultCachePolicy).Returns(cachePolicyMock.Object);

        // Mock the Add method for IAppCache to avoid NullReferenceException
        _cacheMock.Setup(c => c.Add(_cacheKeyConstants.TypeCacheKey, It.IsAny<List<Domain.Entity.TypeProduct>>(), It.IsAny<MemoryCacheEntryOptions>()))
                  .Verifiable();
        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _typeRepositoryMock.Verify(x => x.GetById(request.Id), Times.Once);
        _typeRepositoryMock.Verify(x => x.Delete(request.Id), Times.Once);
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Once);
        _cacheMock.Verify(x => x.GetAsync<List<Domain.Entity.TypeProduct>>(_cacheKeyConstants.TypeCacheKey), Times.Once);
        _cacheMock.Verify(x => x.Add(_cacheKeyConstants.TypeCacheKey, It.IsAny<List<Domain.Entity.TypeProduct>>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Once);
        Assert.True(result);
    }

    [Fact]
    public async Task Should_ThrowNotFoundException_OnNonexistentType()
    {
        // Arrange
        var request = _fixture.Create<DeleteTypeCommand>();
        _typeRepositoryMock.Setup(x => x.GetById(request.Id)).ReturnsAsync((Domain.Entity.TypeProduct)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));
        _typeRepositoryMock.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Never);
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Never);
        _cacheMock.Verify(x => x.GetAsync<List<Domain.Entity.TypeProduct>>(_cacheKeyConstants.TypeCacheKey), Times.Never);
        _cacheMock.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Never);
    }
}
