using AutoFixture;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Features.Type.Commands.UpdateType;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.XUnitTest.Handler.Type.Commands.UpdateType;
public class UpdateTypeCommandHandlerTest : TestSetup
{
    private readonly Mock<ITypeQueries> _mockTypeQueries;
    private readonly UpdateTypeCommandHandler _handler;
    private readonly Mock<IGenericRepository<Domain.Entity.TypeProduct>> _typeRepositoryMock;

    public UpdateTypeCommandHandlerTest()
    {
        _typeRepositoryMock = new Mock<IGenericRepository<Domain.Entity.TypeProduct>>();
        _mockUnitOfWork.Setup(uow => uow.GetBaseRepo<Domain.Entity.TypeProduct>()).Returns(_typeRepositoryMock.Object);
        _mockTypeQueries = new Mock<ITypeQueries>();
        _handler = new UpdateTypeCommandHandler(_mockUnitOfWork.Object, _mapperConfig, _mockTypeQueries.Object, _cacheMock.Object, _cacheKeyConstants);
    }

    [Fact]
    public async Task Should_UpdateType_AndReturnMappedResponse_OnValidData()
    {
        // Arrange
        var request = _fixture.Create<UpdateTypeCommand>();
        var existingType = _fixture.Create<Domain.Entity.TypeProduct>();
        existingType.Id = request.Id;
        _mockTypeQueries.Setup(x => x.GetTypeByIdAsync(request.Id)).ReturnsAsync(existingType);
        _mockTypeQueries.Setup(x => x.CheckIsTypeExisted(request.Name)).ReturnsAsync(false);
        _mockUnitOfWork.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

        var cacheValues = _fixture.CreateMany<Domain.Entity.TypeProduct>().ToList();
        cacheValues.Add(existingType);
        _cacheMock.Setup(c => c.GetAsync<List<Domain.Entity.TypeProduct>>(_cacheKeyConstants.TypeCacheKey)).ReturnsAsync(cacheValues);
        // Mocking DefaultCachePolicy and BuildOptions
        var cachePolicyMock = new Mock<CacheDefaults>();

        _cacheMock.Setup(c => c.DefaultCachePolicy).Returns(cachePolicyMock.Object);

        // Mock the Add method for IAppCache to avoid NullReferenceException
        _cacheMock.Setup(c => c.Add(It.IsAny<string>(), It.IsAny<List<Domain.Entity.TypeProduct>>(), It.IsAny<MemoryCacheEntryOptions>()))
                  .Verifiable();

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _mockTypeQueries.Verify(x => x.GetTypeByIdAsync(request.Id), Times.Once);
        _mockTypeQueries.Verify(x => x.CheckIsTypeExisted(request.Name), Times.Once);
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Once);
        _cacheMock.Verify(x => x.GetAsync<List<Domain.Entity.TypeProduct>>(_cacheKeyConstants.TypeCacheKey), Times.Once);
        _cacheMock.Verify(c => c.Add(_cacheKeyConstants.TypeCacheKey, It.IsAny<List<Domain.Entity.TypeProduct>>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Once);
        Assert.Equal(request.Id, result.Id);
    }

    [Fact]
    public async Task Should_ThrowNotFoundException_OnTypeNotFound()
    {
        // Arrange
        var request = _fixture.Create<UpdateTypeCommand>();
        _mockTypeQueries.Setup(x => x.GetTypeByIdAsync(request.Id)).ReturnsAsync((Domain.Entity.TypeProduct)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Never);
        _cacheMock.Verify(x => x.GetAsync<List<Domain.Entity.TypeProduct>>(_cacheKeyConstants.TypeCacheKey), Times.Never);
        _cacheMock.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Never);
    }

    [Fact]
    public async Task Should_ThrowConflictException_OnExistingTypeName()
    {
        // Arrange
        var request = _fixture.Create<UpdateTypeCommand>();
        var existingType = _fixture.Build<Domain.Entity.TypeProduct>()
                            .Create();
        _mockTypeQueries.Setup(x => x.GetTypeByIdAsync(request.Id)).ReturnsAsync(existingType);
        _mockTypeQueries.Setup(x => x.CheckIsTypeExisted(request.Name)).ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<ConflictException>(async () => await _handler.Handle(request, CancellationToken.None));
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Never);
        _cacheMock.Verify(x => x.GetAsync<List<Domain.Entity.TypeProduct>>(_cacheKeyConstants.TypeCacheKey), Times.Never);
        _cacheMock.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Never);
    }
}
