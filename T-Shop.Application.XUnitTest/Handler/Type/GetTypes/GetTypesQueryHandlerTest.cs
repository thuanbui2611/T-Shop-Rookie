using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Features.Type.Queries.GetTypes;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.Application.XUnitTest.Handler.Type.GetTypes;
public class GetTypesQueryHandlerTest : TestSetup
{
    private readonly Mock<ITypeQueries> _mockTypeQueries;
    private readonly GetTypesQueryHandler _handler;

    public GetTypesQueryHandlerTest()
    {
        _mockTypeQueries = new Mock<ITypeQueries>();
        _handler = new GetTypesQueryHandler(_mapperConfig, _mockTypeQueries.Object, _cacheMock.Object, _cacheKeyConstants);
    }

    [Fact]
    public async Task Should_Return_MappedTypes_FromCache_WhenCacheExists()
    {
        // Arrange
        var expectedTypes = _fixture.CreateMany<Domain.Entity.TypeProduct>().ToList();
        var expectedResult = _mapperConfig.Map<List<TypeResponseModel>>(expectedTypes);
        var cacheKey = _cacheKeyConstants.TypeCacheKey;
        _cacheMock.Setup(c => c.GetOrAddAsync(
           cacheKey,
           It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.TypeProduct>>>>(),
           It.IsAny<MemoryCacheEntryOptions>()))
           .ReturnsAsync(expectedTypes);

        // Act
        var result = await _handler.Handle(new GetTypesQuery(), CancellationToken.None);

        // Assert
        _cacheMock.Verify(c => c.GetOrAddAsync(
            cacheKey,
            It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.TypeProduct>>>>(),
            It.IsAny<MemoryCacheEntryOptions>()));

        _mockTypeQueries.Verify(x => x.GetTypesAsync(), Times.Never);
        result.Should().BeEquivalentTo(expectedResult);
        _cacheKeyConstants.CacheKeyList.Should().Contain(cacheKey);
    }

    [Fact]
    public async Task Should_Return_MappedTypes_FromTypeQueries_AndCache_WhenCacheEmpty()
    {
        // Arrange
        var expectedTypes = _fixture.CreateMany<Domain.Entity.TypeProduct>().ToList();
        var expectedResult = _mapperConfig.Map<List<TypeResponseModel>>(expectedTypes);
        var cacheKey = _cacheKeyConstants.TypeCacheKey;
        _cacheMock.Setup(c => c.GetOrAddAsync(
            It.IsAny<string>(),
            It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.TypeProduct>>>>(),
            It.IsAny<MemoryCacheEntryOptions>()))
            .Returns(async (string key, Func<ICacheEntry, Task<List<Domain.Entity.TypeProduct>>> valueFactory, MemoryCacheEntryOptions options) =>
            {
                var types = await valueFactory(new Mock<ICacheEntry>().Object);
                return types;
            });

        _mockTypeQueries.Setup(x => x.GetTypesAsync()).ReturnsAsync(expectedTypes);

        // Act
        var result = await _handler.Handle(new GetTypesQuery(), CancellationToken.None);

        // Assert
        _cacheMock.Verify(c => c.GetOrAddAsync(
            It.IsAny<string>(),
            It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.TypeProduct>>>>(),
            It.IsAny<MemoryCacheEntryOptions>()), Times.Once);
        _mockTypeQueries.Verify(x => x.GetTypesAsync(), Times.Once);
        result.Should().BeEquivalentTo(expectedResult);
        _cacheKeyConstants.CacheKeyList.Should().Contain(cacheKey);
    }
}
