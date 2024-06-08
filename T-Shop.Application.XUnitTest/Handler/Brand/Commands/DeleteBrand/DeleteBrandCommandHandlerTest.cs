using AutoFixture;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Features.Brand.Command.DeleteBrand;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.XUnitTest.Handler.Brand.Commands.DeleteBrand;
public class DeleteBrandCommandHandlerTest : TestSetup
{
    private readonly Mock<IGenericRepository<Domain.Entity.Brand>> _brandRepositoryMock;
    private readonly DeleteBrandCommandHandler _handler;

    public DeleteBrandCommandHandlerTest()
    {
        _brandRepositoryMock = new Mock<IGenericRepository<Domain.Entity.Brand>>();
        _mockUnitOfWork.Setup(uow => uow.GetBaseRepo<Domain.Entity.Brand>()).Returns(_brandRepositoryMock.Object);

        _handler = new DeleteBrandCommandHandler(_mockUnitOfWork.Object, _cacheMock.Object, _cacheKeyConstants);
        _mockUnitOfWork.Setup(uow => uow.GetBaseRepo<Domain.Entity.Brand>()).Returns(_brandRepositoryMock.Object);
    }
    [Fact]
    public async Task Should_DeleteBrand_UpdateCache_AndReturnTrue_OnExistingBrand()
    {
        // Arrange
        var request = _fixture.Create<DeleteBrandCommand>();
        var existingBrand = _fixture.Build<Domain.Entity.Brand>()
            .With(b => b.Id, request.ID)
            .Create();
        _brandRepositoryMock.Setup(x => x.GetById(request.ID)).ReturnsAsync(existingBrand);
        _mockUnitOfWork.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);
        var cacheValues = _fixture.CreateMany<Domain.Entity.Brand>().ToList();
        _cacheMock.Setup(c => c.GetAsync<List<Domain.Entity.Brand>>(_cacheKeyConstants.BrandCacheKey)).ReturnsAsync(cacheValues);

        // Mocking DefaultCachePolicy and BuildOptions
        var cachePolicyMock = new Mock<CacheDefaults>();

        _cacheMock.Setup(c => c.DefaultCachePolicy).Returns(cachePolicyMock.Object);

        // Mock the Add method for IAppCache to avoid NullReferenceException
        _cacheMock.Setup(c => c.Add(_cacheKeyConstants.BrandCacheKey, It.IsAny<List<Domain.Entity.Brand>>(), It.IsAny<MemoryCacheEntryOptions>()))
                  .Verifiable();
        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _brandRepositoryMock.Verify(x => x.GetById(request.ID), Times.Once);
        _brandRepositoryMock.Verify(x => x.Delete(request.ID), Times.Once);
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Once);
        _cacheMock.Verify(x => x.GetAsync<List<Domain.Entity.Brand>>(_cacheKeyConstants.BrandCacheKey), Times.Once);
        _cacheMock.Verify(x => x.Add(_cacheKeyConstants.BrandCacheKey, It.IsAny<List<Domain.Entity.Brand>>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Once);
        Assert.True(result);
    }

    [Fact]
    public async Task Should_ThrowNotFoundException_OnNonexistentBrand()
    {
        // Arrange
        var request = _fixture.Create<DeleteBrandCommand>();
        _brandRepositoryMock.Setup(x => x.GetById(request.ID)).ReturnsAsync((Domain.Entity.Brand)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));
        _brandRepositoryMock.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Never);
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Never);
        _cacheMock.Verify(x => x.GetAsync<List<Domain.Entity.Brand>>(_cacheKeyConstants.BrandCacheKey), Times.Never);
        _cacheMock.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Never);
    }
}
