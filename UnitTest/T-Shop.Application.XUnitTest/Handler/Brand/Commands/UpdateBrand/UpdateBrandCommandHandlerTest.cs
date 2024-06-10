using AutoFixture;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Features.Brand.Command.UpdateBrand;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.XUnitTest.Handler.Brand.Commands.UpdateBrand;
public class UpdateBrandCommandHandlerTest : TestSetup
{
    private readonly Mock<IBrandQueries> _mockBrandQueries;
    private readonly UpdateBrandCommandHandler _handler;
    private readonly Mock<IGenericRepository<Domain.Entity.Brand>> _brandRepositoryMock;

    public UpdateBrandCommandHandlerTest()
    {
        _brandRepositoryMock = new Mock<IGenericRepository<Domain.Entity.Brand>>();
        _mockUnitOfWork.Setup(uow => uow.GetBaseRepo<Domain.Entity.Brand>()).Returns(_brandRepositoryMock.Object);
        _mockBrandQueries = new Mock<IBrandQueries>();
        _handler = new UpdateBrandCommandHandler(_mockBrandQueries.Object, _mockUnitOfWork.Object, _mapperConfig, _cacheMock.Object, _cacheKeyConstants);
    }

    [Fact]
    public async Task Should_UpdateBrand_AndReturnMappedResponse_OnValidData()
    {
        // Arrange
        var request = _fixture.Create<UpdateBrandCommand>();
        var existingBrand = _fixture.Create<Domain.Entity.Brand>();
        existingBrand.Id = request.ID;
        _mockBrandQueries.Setup(x => x.GetBrandByIdAsync(request.ID)).ReturnsAsync(existingBrand);
        _mockBrandQueries.Setup(x => x.CheckIsBrandExisted(request.Name)).ReturnsAsync(false);
        _mockUnitOfWork.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

        var cacheValues = _fixture.CreateMany<Domain.Entity.Brand>().ToList();
        cacheValues.Add(existingBrand);
        _cacheMock.Setup(c => c.GetAsync<List<Domain.Entity.Brand>>(_cacheKeyConstants.BrandCacheKey)).ReturnsAsync(cacheValues);
        // Mocking DefaultCachePolicy and BuildOptions
        var cachePolicyMock = new Mock<CacheDefaults>();

        _cacheMock.Setup(c => c.DefaultCachePolicy).Returns(cachePolicyMock.Object);

        // Mock the Add method for IAppCache to avoid NullReferenceException
        _cacheMock.Setup(c => c.Add(It.IsAny<string>(), It.IsAny<List<Domain.Entity.Brand>>(), It.IsAny<MemoryCacheEntryOptions>()))
                  .Verifiable();

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _mockBrandQueries.Verify(x => x.GetBrandByIdAsync(request.ID), Times.Once);
        _mockBrandQueries.Verify(x => x.CheckIsBrandExisted(request.Name), Times.Once);
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Once);
        _cacheMock.Verify(x => x.GetAsync<List<Domain.Entity.Brand>>(_cacheKeyConstants.BrandCacheKey), Times.Once);
        _cacheMock.Verify(c => c.Add(_cacheKeyConstants.BrandCacheKey, It.IsAny<List<Domain.Entity.Brand>>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Once);
        Assert.Equal(request.ID, result.ID);
    }

    [Fact]
    public async Task Should_ThrowNotFoundException_OnBrandNotFound()
    {
        // Arrange
        var request = _fixture.Create<UpdateBrandCommand>();
        _mockBrandQueries.Setup(x => x.GetBrandByIdAsync(request.ID)).ReturnsAsync((Domain.Entity.Brand)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Never);
        _cacheMock.Verify(x => x.GetAsync<List<Domain.Entity.Brand>>(_cacheKeyConstants.BrandCacheKey), Times.Never);
        _cacheMock.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Never);
    }

    [Fact]
    public async Task Should_ThrowConflictException_OnExistingBrandName()
    {
        // Arrange
        var request = _fixture.Create<UpdateBrandCommand>();
        var existingBrand = _fixture.Build<Domain.Entity.Brand>()
                            .Create();
        _mockBrandQueries.Setup(x => x.GetBrandByIdAsync(request.ID)).ReturnsAsync(existingBrand);
        _mockBrandQueries.Setup(x => x.CheckIsBrandExisted(request.Name)).ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<ConflictException>(async () => await _handler.Handle(request, CancellationToken.None));
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Never);
        _cacheMock.Verify(x => x.GetAsync<List<Domain.Entity.Brand>>(_cacheKeyConstants.BrandCacheKey), Times.Never);
        _cacheMock.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Never);
    }
}
