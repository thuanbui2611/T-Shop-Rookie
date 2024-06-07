using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Common.Helpers;
using T_Shop.Application.Features.Brand.Queries.GetBrands;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Brand.ResponseModel;

namespace T_Shop.Application.XUnitTest.Handler.Brands.GetBrandsPagination;
public class GetBrandsPaginationQueryHandlerTest : TestSetup
{
    private readonly Mock<IBrandQueries> _brandQueriesMock;
    private readonly GetBrandsPaginationQueryHandler _handler;

    public GetBrandsPaginationQueryHandlerTest()
    {
        _brandQueriesMock = new Mock<IBrandQueries>();
        _handler = new GetBrandsPaginationQueryHandler(_mapperConfig, _brandQueriesMock.Object, _cacheMock.Object, _cacheKeyConstants);
    }

    [Fact]
    public async Task Should_Return_MappedBrandsAndPagination_FromCache_WhenCacheExists()
    {
        // Arrange
        var query = _fixture.Build<GetBrandsPaginationQuery>().Create();
        var brands = _fixture.CreateMany<Domain.Entity.Brand>().ToList();
        var cacheKey = _cacheKeyConstants.BrandCacheKey;

        var (expectedBrandsPaginated, expectedPagination) = PaginationHelpers.GetPaginationModel(brands, query.Pagination);
        var expectedResult = _mapperConfig.Map<List<BrandResponseModel>>(expectedBrandsPaginated);

        _cacheMock.Setup(c => c.GetOrAddAsync(
            cacheKey,
            It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.Brand>>>>(),
            It.IsAny<MemoryCacheEntryOptions>()))
            .ReturnsAsync(brands);

        _brandQueriesMock.Setup(repo => repo.GetBrandsAsync()).ReturnsAsync(brands);

        // Act
        var (brandsResult, paginationResult) = await _handler.Handle(query, CancellationToken.None);

        // Assert
        brandsResult.Should().NotBeNull();
        brandsResult.Should().BeEquivalentTo(expectedResult);

        paginationResult.Should().NotBeNull();
        paginationResult.PageSize.Should().Be(query.Pagination.pageSize);
        paginationResult.CurrentPage.Should().Be(query.Pagination.pageNumber);

        _brandQueriesMock.Verify(repo => repo.GetBrandsAsync(), Times.Never());

        _cacheMock.Verify(c => c.GetOrAddAsync(
          cacheKey,
          It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.Brand>>>>(),
          It.IsAny<MemoryCacheEntryOptions>()));

        // Verify that the cache key was added to the list
        _cacheKeyConstants.CacheKeyList.Should().Contain(cacheKey);
    }

    [Fact]
    public async Task Should_Return_MappedBrandsAndPagination_AfterSearch_FromBrandQueries_AndCache_WhenCacheEmpty()
    {
        // Arrange
        var query = _fixture.Build<GetBrandsPaginationQuery>().Create();
        var brands = _fixture.CreateMany<Domain.Entity.Brand>().ToList();
        var cacheKey = _cacheKeyConstants.BrandCacheKey;

        var (expectedBrandsPaginated, expectedPagination) = PaginationHelpers.GetPaginationModel(brands, query.Pagination);
        var expectedResult = _mapperConfig.Map<List<BrandResponseModel>>(expectedBrandsPaginated);

        _cacheMock.Setup(c => c.GetOrAddAsync(
            It.IsAny<string>(),
            It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.Brand>>>>(),
            It.IsAny<MemoryCacheEntryOptions>()))
            .Returns(async (string key, Func<ICacheEntry, Task<List<Domain.Entity.Brand>>> valueFactory, MemoryCacheEntryOptions options) =>
            {
                var brands = await valueFactory(new Mock<ICacheEntry>().Object);
                return brands;
            });

        _brandQueriesMock.Setup(repo => repo.GetBrandsAsync()).ReturnsAsync(brands);

        // Act
        var (brandsResult, paginationResult) = await _handler.Handle(query, CancellationToken.None);

        // Assert
        brandsResult.Should().NotBeNull();
        brandsResult.Should().BeEquivalentTo(expectedResult);

        paginationResult.Should().NotBeNull();
        paginationResult.PageSize.Should().Be(query.Pagination.pageSize);
        paginationResult.CurrentPage.Should().Be(query.Pagination.pageNumber);

        _brandQueriesMock.Verify(repo => repo.GetBrandsAsync(), Times.Once());

        _cacheMock.Verify(c => c.GetOrAddAsync(
          cacheKey,
          It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.Brand>>>>(),
          It.IsAny<MemoryCacheEntryOptions>()));

        // Verify that the cache key was added to the list
        _cacheKeyConstants.CacheKeyList.Should().Contain(cacheKey);
    }
}
