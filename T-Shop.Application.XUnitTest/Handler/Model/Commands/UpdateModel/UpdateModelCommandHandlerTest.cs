using AutoFixture;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Features.ModelProduct.Commands.UpdateModelProduct;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.XUnitTest.Handler.Model.Commands.UpdateModel;
public class UpdateModelCommandHandlerTest : TestSetup
{
    private readonly Mock<IModelQueries> _mockModelQueries;
    private readonly Mock<IBrandQueries> _mockBrandQueries;
    private readonly Mock<IGenericRepository<Domain.Entity.Model>> _modelRepositoryMock;
    private readonly UpdateModelProductCommandHandler _handler;

    public UpdateModelCommandHandlerTest()
    {
        _modelRepositoryMock = new Mock<IGenericRepository<Domain.Entity.Model>>();
        _mockUnitOfWork.Setup(uow => uow.GetBaseRepo<Domain.Entity.Model>()).Returns(_modelRepositoryMock.Object);
        _mockModelQueries = new Mock<IModelQueries>();
        _mockBrandQueries = new Mock<IBrandQueries>();
        _handler = new UpdateModelProductCommandHandler(_mockModelQueries.Object, _mockUnitOfWork.Object, _mapperConfig, _mockBrandQueries.Object, _cacheMock.Object, _cacheKeyConstants);
    }

    [Fact]
    public async Task Should_UpdateModel_UpdateCache_AndReturnResponse_OnValidData()
    {
        // Arrange
        var request = _fixture.Create<UpdateModelProductCommand>();
        var existingModel = _fixture.CreateMany<Domain.Entity.Model>().ToList(); // No existing model with same year

        _mockModelQueries.Setup(x => x.GetModelsByNameAsync(request.Name)).ReturnsAsync(existingModel);
        var brand = _fixture.Create<Domain.Entity.Brand>();
        _mockBrandQueries.Setup(x => x.GetBrandByIdAsync(request.BrandID)).ReturnsAsync(brand);
        _mockUnitOfWork.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

        var mappedModel = _mapperConfig.Map<Domain.Entity.Model>(request); // Mapped model
        var cacheValues = _fixture.CreateMany<Domain.Entity.Model>().ToList();
        cacheValues.Add(mappedModel);
        _cacheMock.Setup(c => c.GetAsync<List<Domain.Entity.Model>>(_cacheKeyConstants.ModelCacheKey)).ReturnsAsync(cacheValues);
        // Mocking DefaultCachePolicy and BuildOptions
        var cachePolicyMock = new Mock<CacheDefaults>();

        _cacheMock.Setup(c => c.DefaultCachePolicy).Returns(cachePolicyMock.Object);
        _cacheMock.Setup(c => c.Add(It.IsAny<string>(), It.IsAny<List<Domain.Entity.Model>>(), It.IsAny<MemoryCacheEntryOptions>()))
                 .Verifiable();
        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _mockModelQueries.Verify(x => x.GetModelsByNameAsync(request.Name), Times.Once);
        _mockBrandQueries.Verify(x => x.GetBrandByIdAsync(request.BrandID), Times.Once);

        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Once);
        _cacheMock.Verify(x => x.GetAsync<List<Domain.Entity.Model>>(_cacheKeyConstants.ModelCacheKey), Times.Once);
        _cacheMock.Verify(x => x.Add(_cacheKeyConstants.ModelCacheKey, It.IsAny<List<Domain.Entity.Model>>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Once);
        Assert.NotNull(result); // Assuming Domain.Entity.ModelProductResponseModel has properties mapped from Model
    }

    [Fact]
    public async Task Should_ThrowConflictException_OnExistingModelWithSameYear()
    {
        // Arrange
        var request = _fixture.Create<UpdateModelProductCommand>();
        var existingModel = new List<Domain.Entity.Model>() { new Domain.Entity.Model { Name = request.Name, Year = request.Year } };
        _mockModelQueries.Setup(x => x.GetModelsByNameAsync(request.Name)).ReturnsAsync(existingModel);

        // Act & Assert
        await Assert.ThrowsAsync<ConflictException>(async () => await _handler.Handle(request, CancellationToken.None));
        _modelRepositoryMock.Verify(x => x.Update(It.IsAny<Domain.Entity.Model>()), Times.Never);
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Never);
        _cacheMock.Verify(x => x.GetAsync<List<Domain.Entity.Model>>(_cacheKeyConstants.ModelCacheKey), Times.Never);
        _cacheMock.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Never);
    }

    [Fact]
    public async Task Should_ThrowNotFoundException_OnNonexistentBrand()
    {
        // Arrange
        var request = _fixture.Create<UpdateModelProductCommand>();
        _mockModelQueries.Setup(x => x.GetModelsByNameAsync(request.Name)).ReturnsAsync(new List<Domain.Entity.Model>());
        _mockBrandQueries.Setup(x => x.GetBrandByIdAsync(request.BrandID)).ReturnsAsync((Domain.Entity.Brand)null);
        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));
        _modelRepositoryMock.Verify(x => x.Update(It.IsAny<Domain.Entity.Model>()), Times.Never);
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Never);
        _cacheMock.Verify(x => x.GetAsync<List<Domain.Entity.Model>>(_cacheKeyConstants.ModelCacheKey), Times.Never);
        _cacheMock.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Never);
    }
}
