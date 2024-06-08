using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Features.ModelProduct.Queries.GetModelProducts;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;

namespace T_Shop.Application.XUnitTest.Handler.Model.GetModels;
public class GetModelsQueryHandlerTest : TestSetup
{
    private readonly Mock<IModelQueries> _mockModelQueries;
    private readonly GetModelsQueryHandler _handler;

    public GetModelsQueryHandlerTest()
    {
        _mockModelQueries = new Mock<IModelQueries>();
        _handler = new GetModelsQueryHandler(_mapperConfig, _mockModelQueries.Object, _cacheMock.Object, _cacheKeyConstants);
    }

    [Fact]
    public async Task Should_Return_MappedModels_FromCache_WhenCacheExists()
    {
        // Arrange
        var expectedModels = _fixture.CreateMany<Domain.Entity.Model>().ToList();
        var expectedResult = _mapperConfig.Map<List<ModelProductResponseModel>>(expectedModels);
        var cacheKey = _cacheKeyConstants.ModelCacheKey;
        _cacheMock.Setup(c => c.GetOrAddAsync(
           cacheKey,
           It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.Model>>>>(),
           It.IsAny<MemoryCacheEntryOptions>()))
           .ReturnsAsync(expectedModels);

        // Act
        var result = await _handler.Handle(new GetModelsQuery(), CancellationToken.None);

        // Assert
        _cacheMock.Verify(c => c.GetOrAddAsync(
            cacheKey,
            It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.Model>>>>(),
            It.IsAny<MemoryCacheEntryOptions>()));

        _mockModelQueries.Verify(x => x.GetModelsAsync(), Times.Never); // Not called since cache is used
        result.Should().BeEquivalentTo(expectedResult);
        _cacheKeyConstants.CacheKeyList.Should().Contain(cacheKey);
    }

    [Fact]
    public async Task Should_Return_MappedModels_FromModelQueries_AndCache_WhenCacheEmpty()
    {
        // Arrange
        var expectedModels = _fixture.CreateMany<Domain.Entity.Model>().ToList();
        var expectedResult = _mapperConfig.Map<List<ModelProductResponseModel>>(expectedModels);
        var cacheKey = _cacheKeyConstants.ModelCacheKey;
        _cacheMock.Setup(c => c.GetOrAddAsync(
            It.IsAny<string>(),
            It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.Model>>>>(),
            It.IsAny<MemoryCacheEntryOptions>()))
            .Returns(async (string key, Func<ICacheEntry, Task<List<Domain.Entity.Model>>> valueFactory, MemoryCacheEntryOptions options) =>
            {
                var models = await valueFactory(new Mock<ICacheEntry>().Object);
                return models;
            });

        _mockModelQueries.Setup(x => x.GetModelsAsync()).ReturnsAsync(expectedModels);

        // Act
        var result = await _handler.Handle(new GetModelsQuery(), CancellationToken.None);

        // Assert
        _cacheMock.Verify(c => c.GetOrAddAsync(
             It.IsAny<string>(),
            It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.Model>>>>(),
            It.IsAny<MemoryCacheEntryOptions>()), Times.Once);
        _mockModelQueries.Verify(x => x.GetModelsAsync(), Times.Once);
        result.Should().BeEquivalentTo(expectedResult);
        _cacheKeyConstants.CacheKeyList.Should().Contain(cacheKey);
    }
}
