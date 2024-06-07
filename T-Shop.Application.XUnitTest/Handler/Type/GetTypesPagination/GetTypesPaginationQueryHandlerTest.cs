using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Common.Helpers;
using T_Shop.Application.Features.Type.Queries.GetTypes;
using T_Shop.Application.Features.Type.Queries.GetTypesPagination;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.Application.XUnitTest.Handler.Type.GetTypesPagination;
public class GetTypesPaginationQueryHandlerTest : TestSetup
{
    private readonly Mock<ITypeQueries> _typeQueriesMock;
    private readonly GetTypesPaginationQueryHandler _handler;

    public GetTypesPaginationQueryHandlerTest()
    {
        _typeQueriesMock = new Mock<ITypeQueries>();
        _handler = new GetTypesPaginationQueryHandler(_mapperConfig, _typeQueriesMock.Object, _cacheMock.Object, _cacheKeyConstants);
    }

    [Fact]
    public async Task Should_Return_MappedTypesAndPagination_FromCache_WhenCacheExists()
    {
        // Arrange
        var query = _fixture.Build<GetTypesPaginationQuery>().Create();
        var types = _fixture.CreateMany<Domain.Entity.TypeProduct>().ToList();
        var cacheKey = _cacheKeyConstants.TypeCacheKey;

        var (expectedTypesPaginated, expectedPagination) = PaginationHelpers.GetPaginationModel(types, query.Pagination);
        var expectedResult = _mapperConfig.Map<List<TypeResponseModel>>(expectedTypesPaginated);

        _cacheMock.Setup(c => c.GetOrAddAsync(
            cacheKey,
            It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.TypeProduct>>>>(),
            It.IsAny<MemoryCacheEntryOptions>()))
            .ReturnsAsync(types);

        _typeQueriesMock.Setup(repo => repo.GetTypesAsync()).ReturnsAsync(types);

        // Act
        var (typesResult, paginationResult) = await _handler.Handle(query, CancellationToken.None);

        // Assert
        typesResult.Should().NotBeNull();
        typesResult.Should().BeEquivalentTo(expectedResult);

        paginationResult.Should().NotBeNull();
        paginationResult.PageSize.Should().Be(query.Pagination.pageSize);
        paginationResult.CurrentPage.Should().Be(query.Pagination.pageNumber);

        _typeQueriesMock.Verify(repo => repo.GetTypesAsync(), Times.Never());

        _cacheMock.Verify(c => c.GetOrAddAsync(
          cacheKey,
          It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.TypeProduct>>>>(),
          It.IsAny<MemoryCacheEntryOptions>()));

        // Verify that the cache key was added to the list
        _cacheKeyConstants.CacheKeyList.Should().Contain(cacheKey);
    }

    [Fact]
    public async Task Should_Return_MappedTypesAndPagination_AfterSearch_FromTypeQueries_AndCache_WhenCacheEmpty()
    {
        // Arrange
        var query = _fixture.Build<GetTypesPaginationQuery>().Create();
        var types = _fixture.CreateMany<Domain.Entity.TypeProduct>().ToList();
        var cacheKey = _cacheKeyConstants.TypeCacheKey;

        var (expectedTypesPaginated, expectedPagination) = PaginationHelpers.GetPaginationModel(types, query.Pagination);
        var expectedResult = _mapperConfig.Map<List<TypeResponseModel>>(expectedTypesPaginated);

        _cacheMock.Setup(c => c.GetOrAddAsync(
            It.IsAny<string>(),
            It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.TypeProduct>>>>(),
            It.IsAny<MemoryCacheEntryOptions>()))
            .Returns(async (string key, Func<ICacheEntry, Task<List<Domain.Entity.TypeProduct>>> valueFactory, MemoryCacheEntryOptions options) =>
            {
                var types = await valueFactory(new Mock<ICacheEntry>().Object);
                return types;
            });

        _typeQueriesMock.Setup(repo => repo.GetTypesAsync()).ReturnsAsync(types);

        // Act
        var (typesResult, paginationResult) = await _handler.Handle(query, CancellationToken.None);

        // Assert
        typesResult.Should().NotBeNull();
        typesResult.Should().BeEquivalentTo(expectedResult);

        paginationResult.Should().NotBeNull();
        paginationResult.PageSize.Should().Be(query.Pagination.pageSize);
        paginationResult.CurrentPage.Should().Be(query.Pagination.pageNumber);

        _typeQueriesMock.Verify(repo => repo.GetTypesAsync(), Times.Once());

        _cacheMock.Verify(c => c.GetOrAddAsync(
          cacheKey,
          It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.TypeProduct>>>>(),
          It.IsAny<MemoryCacheEntryOptions>()));

        // Verify that the cache key was added to the list
        _cacheKeyConstants.CacheKeyList.Should().Contain(cacheKey);
    }
}
