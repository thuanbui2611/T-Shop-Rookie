using AutoFixture;
using AutoMapper;
using FluentAssertions;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Common.Constants;
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
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IAppCache> _cacheMock;
    private readonly CacheKeyConstants _cacheKeyConstants;

    public GetProductsQueryHandlerTest()
    {
        _productQueriesMock = new Mock<IProductQueries>();
        _cacheKeyConstants = new CacheKeyConstants();
        //_mapper = new MapperConfiguration(cfg =>
        //{
        //    cfg.AddProfile<MappingProfile>();
        //}).CreateMapper();
        //_cache = new ;
        _cacheMock = new Mock<IAppCache>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetProductsQueryHandler(_productQueriesMock.Object, _mapperMock.Object, _cacheMock.Object, _cacheKeyConstants);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginatedListOfProductDto_WhenProductsExist()
    {
        // Arrange
        var query = _fixture.Build<GetProductsQuery>().Create();
        var products = _fixture.CreateMany<Product>().ToList();
        var cacheKey = _cacheKeyConstants.ProductCacheKey;

        var (expectedProductsPaginated, expectedPagination) = PaginationHelpers.GetPaginationModel(products, query.Pagination);

        _cacheMock.Setup(c => c.GetOrAddAsync(
                It.IsAny<string>(),
                It.IsAny<Func<ICacheEntry, Task<List<Product>>>>(),
                It.IsAny<MemoryCacheEntryOptions>()))
            .ReturnsAsync(products);
        //_cacheMock.Setup(c => c.DefaultCachePolicy).Returns(new CacheDefaults());

        _productQueriesMock.Setup(repo => repo.GetAllProductsAsync()).ReturnsAsync(products);

        _mapperMock.Setup(m => m.Map<List<ProductResponseModel>>(It.IsAny<IEnumerable<Product>>()))
                   .Returns(expectedProductsPaginated.Select(p => new ProductResponseModel { Id = p.Id }).ToList());

        // Act
        var (productsResult, paginationResult) = await _handler.Handle(query, CancellationToken.None);

        // Assert
        paginationResult.Should().NotBeNull();
        paginationResult.PageSize.Should().Be(query.Pagination.pageSize);
        paginationResult.CurrentPage.Should().Be(query.Pagination.pageNumber);

        _productQueriesMock.Verify(repo => repo.GetAllProductsAsync(), Times.AtMostOnce());
        _mapperMock.Verify(m => m.Map<List<ProductResponseModel>>(It.IsAny<IEnumerable<Product>>()), Times.Once());

        // Verify that the cache key was added to the list
        _cacheKeyConstants.CacheKeyList.Should().Contain(cacheKey);
    }


}
