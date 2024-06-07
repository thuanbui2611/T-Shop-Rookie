using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Features.Brand.Queries.GetBrands;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Brand.ResponseModel;

namespace T_Shop.Application.XUnitTest.Handler.Brands.GetBrands;
public class GetBrandsQueryHandlerTest : TestSetup
{
    private readonly Mock<IBrandQueries> _mockBrandQueries;
    private readonly GetBrandsQueryHandler _handler;

    public GetBrandsQueryHandlerTest()
    {
        _mockBrandQueries = new Mock<IBrandQueries>();
        _handler = new GetBrandsQueryHandler(_mapperConfig, _mockBrandQueries.Object, _cacheMock.Object, _cacheKeyConstants);
    }

    [Fact]
    public async Task Should_Return_MappedBrands_FromCache_WhenCacheExists()
    {
        // Arrange
        var expectedBrands = _fixture.CreateMany<Domain.Entity.Brand>().ToList();
        var expectedResult = _mapperConfig.Map<List<BrandResponseModel>>(expectedBrands);
        var cacheKey = _cacheKeyConstants.BrandCacheKey;
        _cacheMock.Setup(c => c.GetOrAddAsync(
           cacheKey,
           It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.Brand>>>>(),
           It.IsAny<MemoryCacheEntryOptions>()))
           .ReturnsAsync(expectedBrands);

        // Act
        var result = await _handler.Handle(new GetBrandsQuery(), CancellationToken.None);

        // Assert
        _cacheMock.Verify(c => c.GetOrAddAsync(
            cacheKey,
            It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.Brand>>>>(),
            It.IsAny<MemoryCacheEntryOptions>()));

        _mockBrandQueries.Verify(x => x.GetBrandsAsync(), Times.Never); // Not called since cache is used
        result.Should().BeEquivalentTo(expectedResult);
        _cacheKeyConstants.CacheKeyList.Should().Contain(cacheKey);
    }

    [Fact]
    public async Task Should_Return_MappedBrands_FromBrandQueries_AndCache_WhenCacheEmpty()
    {
        // Arrange
        var expectedBrands = _fixture.CreateMany<Domain.Entity.Brand>().ToList();
        var expectedResult = _mapperConfig.Map<List<BrandResponseModel>>(expectedBrands);
        var cacheKey = _cacheKeyConstants.BrandCacheKey;
        _cacheMock.Setup(c => c.GetOrAddAsync(
            It.IsAny<string>(),
            It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.Brand>>>>(),
            It.IsAny<MemoryCacheEntryOptions>()))
            .Returns(async (string key, Func<ICacheEntry, Task<List<Domain.Entity.Brand>>> valueFactory, MemoryCacheEntryOptions options) =>
            {
                var brands = await valueFactory(new Mock<ICacheEntry>().Object);
                return brands;
            });

        _mockBrandQueries.Setup(x => x.GetBrandsAsync()).ReturnsAsync(expectedBrands);

        // Act
        var result = await _handler.Handle(new GetBrandsQuery(), CancellationToken.None);

        // Assert
        _cacheMock.Verify(c => c.GetOrAddAsync(
             It.IsAny<string>(),
            It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.Brand>>>>(),
            It.IsAny<MemoryCacheEntryOptions>()), Times.Once);
        _mockBrandQueries.Verify(x => x.GetBrandsAsync(), Times.Once);
        result.Should().BeEquivalentTo(expectedResult);
        _cacheKeyConstants.CacheKeyList.Should().Contain(cacheKey);
    }
}
