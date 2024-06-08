using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Features.Color.Queries.GetColors;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Color.ResponseModel;

namespace T_Shop.Application.XUnitTest.Handler.Color.Queries.GetColors;
public class GetColorsQueryHandlerTest : TestSetup
{
    private readonly Mock<IColorQueries> _mockColorQueries;
    private readonly GetColorsQueryHandler _handler;

    public GetColorsQueryHandlerTest()
    {
        _mockColorQueries = new Mock<IColorQueries>();
        _handler = new GetColorsQueryHandler(_mapperConfig, _mockColorQueries.Object, _cacheMock.Object, _cacheKeyConstants);
    }

    [Fact]
    public async Task Should_Return_MappedColors_FromCache_WhenCacheExists()
    {
        // Arrange
        var expectedColors = _fixture.CreateMany<Domain.Entity.Color>().ToList();
        var expectedResult = _mapperConfig.Map<List<ColorResponseModel>>(expectedColors);
        var cacheKey = _cacheKeyConstants.ColorCacheKey;
        _cacheMock.Setup(c => c.GetOrAddAsync(
           cacheKey,
           It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.Color>>>>(),
           It.IsAny<MemoryCacheEntryOptions>()))
           .ReturnsAsync(expectedColors);

        // Act
        var result = await _handler.Handle(new GetColorsQuery(), CancellationToken.None);

        // Assert
        _cacheMock.Verify(c => c.GetOrAddAsync(
            cacheKey,
            It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.Color>>>>(),
            It.IsAny<MemoryCacheEntryOptions>()));

        _mockColorQueries.Verify(x => x.GetColorsAsync(), Times.Never); // Not called since cache is used
        result.Should().BeEquivalentTo(expectedResult);
        _cacheKeyConstants.CacheKeyList.Should().Contain(cacheKey);
    }

    [Fact]
    public async Task Should_Return_MappedColors_FromColorQueries_AndCache_WhenCacheEmpty()
    {
        // Arrange
        var expectedColors = _fixture.CreateMany<Domain.Entity.Color>().ToList();
        var expectedResult = _mapperConfig.Map<List<ColorResponseModel>>(expectedColors);
        var cacheKey = _cacheKeyConstants.ColorCacheKey;
        _cacheMock.Setup(c => c.GetOrAddAsync(
            It.IsAny<string>(),
            It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.Color>>>>(),
            It.IsAny<MemoryCacheEntryOptions>()))
            .Returns(async (string key, Func<ICacheEntry, Task<List<Domain.Entity.Color>>> valueFactory, MemoryCacheEntryOptions options) =>
            {
                var colors = await valueFactory(new Mock<ICacheEntry>().Object);
                return colors;
            });

        _mockColorQueries.Setup(x => x.GetColorsAsync()).ReturnsAsync(expectedColors);

        // Act
        var result = await _handler.Handle(new GetColorsQuery(), CancellationToken.None);

        // Assert
        _cacheMock.Verify(c => c.GetOrAddAsync(
             It.IsAny<string>(),
            It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.Color>>>>(),
            It.IsAny<MemoryCacheEntryOptions>()), Times.Once);
        _mockColorQueries.Verify(x => x.GetColorsAsync(), Times.Once);
        result.Should().BeEquivalentTo(expectedResult);
        _cacheKeyConstants.CacheKeyList.Should().Contain(cacheKey);
    }
}
