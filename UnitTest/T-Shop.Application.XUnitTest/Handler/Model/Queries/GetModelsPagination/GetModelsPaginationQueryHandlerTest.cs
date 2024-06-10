using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Common.Helpers;
using T_Shop.Application.Features.ModelProduct.Queries.GetModelPagination;
using T_Shop.Application.Features.ModelProduct.Queries.GetModelProducts;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;

namespace T_Shop.Application.XUnitTest.Handler.Model.Queries.GetModelsPagination;
public class GetModelsPaginationQueryHandlerTest : TestSetup
{
    private readonly Mock<IModelQueries> _modelQueriesMock;
    private readonly GetModelsPaginationQueryHandler _handler;

    public GetModelsPaginationQueryHandlerTest()
    {
        _modelQueriesMock = new Mock<IModelQueries>();
        _handler = new GetModelsPaginationQueryHandler(_mapperConfig, _modelQueriesMock.Object, _cacheMock.Object, _cacheKeyConstants);
    }

    [Fact]
    public async Task Should_Return_MappedModelsAndPagination_FromCache_WhenCacheExists()
    {
        // Arrange
        var query = _fixture.Build<GetModelsPaginationQuery>().Create();
        var models = _fixture.CreateMany<Domain.Entity.Model>().ToList();
        var cacheKey = _cacheKeyConstants.ModelCacheKey;

        var (expectedModelsPaginated, expectedPagination) = PaginationHelpers.GetPaginationModel(models, query.Pagination);
        var expectedResult = _mapperConfig.Map<List<ModelProductResponseModel>>(expectedModelsPaginated);

        _cacheMock.Setup(c => c.GetOrAddAsync(
            cacheKey,
            It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.Model>>>>(),
            It.IsAny<MemoryCacheEntryOptions>()))
            .ReturnsAsync(models);

        _modelQueriesMock.Setup(repo => repo.GetModelsAsync()).ReturnsAsync(models);

        // Act
        var (modelsResult, paginationResult) = await _handler.Handle(query, CancellationToken.None);

        // Assert
        modelsResult.Should().NotBeNull();
        modelsResult.Should().BeEquivalentTo(expectedResult);

        paginationResult.Should().NotBeNull();
        paginationResult.PageSize.Should().Be(query.Pagination.pageSize);
        paginationResult.CurrentPage.Should().Be(query.Pagination.pageNumber);

        _modelQueriesMock.Verify(repo => repo.GetModelsAsync(), Times.Never());

        _cacheMock.Verify(c => c.GetOrAddAsync(
          cacheKey,
          It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.Model>>>>(),
          It.IsAny<MemoryCacheEntryOptions>()));

        // Verify that the cache key was added to the list
        _cacheKeyConstants.CacheKeyList.Should().Contain(cacheKey);
    }

    [Fact]
    public async Task Should_Return_MappedModelsAndPagination_AfterSearch_FromModelQueries_AndCache_WhenCacheEmpty()
    {
        // Arrange
        var query = _fixture.Build<GetModelsPaginationQuery>().Create();
        var models = _fixture.CreateMany<Domain.Entity.Model>().ToList();
        var cacheKey = _cacheKeyConstants.ModelCacheKey;

        var (expectedModelsPaginated, expectedPagination) = PaginationHelpers.GetPaginationModel(models, query.Pagination);
        var expectedResult = _mapperConfig.Map<List<ModelProductResponseModel>>(expectedModelsPaginated);

        _cacheMock.Setup(c => c.GetOrAddAsync(
            It.IsAny<string>(),
            It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.Model>>>>(),
            It.IsAny<MemoryCacheEntryOptions>()))
            .Returns(async (string key, Func<ICacheEntry, Task<List<Domain.Entity.Model>>> valueFactory, MemoryCacheEntryOptions options) =>
            {
                var models = await valueFactory(new Mock<ICacheEntry>().Object);
                return models;
            });

        _modelQueriesMock.Setup(repo => repo.GetModelsAsync()).ReturnsAsync(models);

        // Act
        var (modelsResult, paginationResult) = await _handler.Handle(query, CancellationToken.None);

        // Assert
        modelsResult.Should().NotBeNull();
        modelsResult.Should().BeEquivalentTo(expectedResult);

        paginationResult.Should().NotBeNull();
        paginationResult.PageSize.Should().Be(query.Pagination.pageSize);
        paginationResult.CurrentPage.Should().Be(query.Pagination.pageNumber);

        _modelQueriesMock.Verify(repo => repo.GetModelsAsync(), Times.Once());

        _cacheMock.Verify(c => c.GetOrAddAsync(
          cacheKey,
          It.IsAny<Func<ICacheEntry, Task<List<Domain.Entity.Model>>>>(),
          It.IsAny<MemoryCacheEntryOptions>()));

        // Verify that the cache key was added to the list
        _cacheKeyConstants.CacheKeyList.Should().Contain(cacheKey);
    }
}
