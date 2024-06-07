using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Common.Helpers;
using T_Shop.Application.Features.Products.Queries.GetProducts;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Product.ResponseModel;

namespace T_Shop.Application.XUnitTest.Handler.Products.GetProducts;
public class GetProductsQueryHandlerTest : TestSetup
{
    private readonly GetProductsQueryHandler _handler;
    private readonly Mock<IProductQueries> _productQueriesMock;

    public GetProductsQueryHandlerTest()
    {
        _productQueriesMock = new Mock<IProductQueries>();
        _handler = new GetProductsQueryHandler(_productQueriesMock.Object, _mapperConfig, _cacheMock.Object, _cacheKeyConstants);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginatedListOfProductDto_WhenCacheEmpty()
    {
        // Arrange
        var query = _fixture.Build<GetProductsQuery>().Create();
        var products = _fixture.CreateMany<Product>().ToList();
        var cacheKey = _cacheKeyConstants.ProductCacheKey;

        var (expectedProductsPaginated, expectedPagination) = PaginationHelpers.GetPaginationModel(products, query.Pagination);
        var expectedResult = _mapperConfig.Map<List<ProductResponseModel>>(expectedProductsPaginated);

        _cacheMock.Setup(c => c.GetOrAddAsync(
            It.IsAny<string>(),
            It.IsAny<Func<ICacheEntry, Task<List<Product>>>>(),
            It.IsAny<MemoryCacheEntryOptions>()))
            .Returns(async (string key, Func<ICacheEntry, Task<List<Product>>> valueFactory, MemoryCacheEntryOptions options) =>
            {
                var products = await valueFactory(new Mock<ICacheEntry>().Object); // Call the actual factory (GetAllProductsAsync)
                return products;
            });

        _productQueriesMock.Setup(repo => repo.GetAllProductsAsync()).ReturnsAsync(products);

        // Act
        var (productsResult, paginationResult) = await _handler.Handle(query, CancellationToken.None);

        // Assert
        productsResult.Should().NotBeNull();
        productsResult.Should().BeEquivalentTo(expectedResult);

        paginationResult.Should().NotBeNull();
        paginationResult.PageSize.Should().Be(query.Pagination.pageSize);
        paginationResult.CurrentPage.Should().Be(query.Pagination.pageNumber);

        _productQueriesMock.Verify(repo => repo.GetAllProductsAsync(), Times.Once());

        _cacheMock.Verify(c => c.GetOrAddAsync(
          cacheKey,
          It.IsAny<Func<ICacheEntry, Task<List<Product>>>>(),
          It.IsAny<MemoryCacheEntryOptions>()));

        // Verify that the cache key was added to the list
        _cacheKeyConstants.CacheKeyList.Should().Contain(cacheKey);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginatedListOfProductDto_WhenCacheExists()
    {
        // Arrange
        var query = _fixture.Build<GetProductsQuery>().Create();
        var products = _fixture.CreateMany<Product>().ToList();
        var cacheKey = _cacheKeyConstants.ProductCacheKey;

        var (expectedProductsPaginated, expectedPagination) = PaginationHelpers.GetPaginationModel(products, query.Pagination);
        var expectedResult = _mapperConfig.Map<List<ProductResponseModel>>(expectedProductsPaginated);

        _cacheMock.Setup(c => c.GetOrAddAsync(
            cacheKey,
            It.IsAny<Func<ICacheEntry, Task<List<Product>>>>(),
            It.IsAny<MemoryCacheEntryOptions>()))
            .ReturnsAsync(products);

        _productQueriesMock.Setup(repo => repo.GetAllProductsAsync()).ReturnsAsync(products);

        // Act
        var (productsResult, paginationResult) = await _handler.Handle(query, CancellationToken.None);

        // Assert
        productsResult.Should().NotBeNull();
        productsResult.Should().BeEquivalentTo(expectedResult);

        paginationResult.Should().NotBeNull();
        paginationResult.PageSize.Should().Be(query.Pagination.pageSize);
        paginationResult.CurrentPage.Should().Be(query.Pagination.pageNumber);

        _productQueriesMock.Verify(repo => repo.GetAllProductsAsync(), Times.Never());

        _cacheMock.Verify(c => c.GetOrAddAsync(
          cacheKey,
          It.IsAny<Func<ICacheEntry, Task<List<Product>>>>(),
          It.IsAny<MemoryCacheEntryOptions>()));

        // Verify that the cache key was added to the list
        _cacheKeyConstants.CacheKeyList.Should().Contain(cacheKey);
    }

}
