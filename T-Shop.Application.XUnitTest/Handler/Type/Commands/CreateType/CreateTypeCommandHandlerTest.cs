using AutoFixture;
using FluentAssertions;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Features.Type.Commands.CreateType;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.XUnitTest.Handler.Type.Commands.CreateType;
public class CreateTypeCommandHandlerTest : TestSetup
{
    private readonly Mock<ITypeQueries> _typeQueriesMock;
    private readonly Mock<IGenericRepository<TypeProduct>> _typeRepositoryMock;
    private readonly CreateTypeCommandHandler _handler;

    public CreateTypeCommandHandlerTest()
    {
        _typeQueriesMock = new Mock<ITypeQueries>();
        _typeRepositoryMock = new Mock<IGenericRepository<TypeProduct>>();

        _mockUnitOfWork.Setup(uow => uow.GetBaseRepo<TypeProduct>()).Returns(_typeRepositoryMock.Object);

        _handler = new CreateTypeCommandHandler(_mapperConfig, _mockUnitOfWork.Object, _typeQueriesMock.Object, _cacheMock.Object, _cacheKeyConstants);
    }

    [Fact]
    public async Task Handle_ShouldAddType_WhenTypeDoesNotExist()
    {
        // Arrange
        var command = _fixture.Create<CreateTypeCommand>();
        var newType = _fixture.Create<TypeProduct>();
        newType.Name = command.Name;

        _typeQueriesMock.Setup(q => q.CheckIsTypeExisted(command.Name)).ReturnsAsync(false);
        _typeRepositoryMock.Setup(repo => repo.Add(It.IsAny<TypeProduct>())).Returns(newType);
        _mockUnitOfWork.Setup(uow => uow.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(command.Name);

        _typeQueriesMock.Verify(q => q.CheckIsTypeExisted(command.Name), Times.Once);
        _typeRepositoryMock.Verify(repo => repo.Add(It.IsAny<TypeProduct>()), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowConflictException_WhenTypeExists()
    {
        // Arrange
        var command = _fixture.Create<CreateTypeCommand>();

        _typeQueriesMock.Setup(q => q.CheckIsTypeExisted(command.Name)).ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<ConflictException>(() => _handler.Handle(command, CancellationToken.None));

        _typeQueriesMock.Verify(q => q.CheckIsTypeExisted(command.Name), Times.Once);
        _typeRepositoryMock.Verify(repo => repo.Add(It.IsAny<TypeProduct>()), Times.Never);
        _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldUpdateCache_WhenTypeIsAdded()
    {
        // Arrange
        var command = _fixture.Create<CreateTypeCommand>();
        var newType = _fixture.Create<TypeProduct>();
        newType.Name = command.Name;

        _typeQueriesMock.Setup(q => q.CheckIsTypeExisted(command.Name)).ReturnsAsync(false);
        _typeRepositoryMock.Setup(repo => repo.Add(It.IsAny<TypeProduct>())).Returns(newType);
        _mockUnitOfWork.Setup(uow => uow.CompleteAsync()).Returns(Task.CompletedTask);

        var cacheValues = _fixture.CreateMany<TypeProduct>().ToList();
        _cacheMock.Setup(c => c.GetAsync<List<TypeProduct>>(_cacheKeyConstants.TypeCacheKey)).ReturnsAsync(cacheValues);

        // Mocking DefaultCachePolicy and BuildOptions
        var cachePolicyMock = new Mock<CacheDefaults>();

        _cacheMock.Setup(c => c.DefaultCachePolicy).Returns(cachePolicyMock.Object);

        // Mock the Add method for IAppCache to avoid NullReferenceException
        _cacheMock.Setup(c => c.Add(It.IsAny<string>(), It.IsAny<List<TypeProduct>>(), It.IsAny<MemoryCacheEntryOptions>()))
                  .Verifiable();
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(command.Name);

        _cacheMock.Verify(c => c.GetAsync<List<TypeProduct>>(_cacheKeyConstants.TypeCacheKey), Times.Once);
        _cacheMock.Verify(c => c.Add(_cacheKeyConstants.TypeCacheKey, It.IsAny<List<TypeProduct>>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Once);
    }

}
