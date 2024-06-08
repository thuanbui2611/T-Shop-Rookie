using AutoFixture;
using FluentAssertions;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Features.Brand.Command.CreateBrand;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.XUnitTest.Handler.Brand.Commands.CreateBrand;
public class CreateBrandCommandHandlerTest : TestSetup
{
    private readonly Mock<IBrandQueries> _brandQueriesMock;
    private readonly Mock<IGenericRepository<Domain.Entity.Brand>> _brandRepositoryMock;
    private readonly CreateBrandCommandHandler _handler;

    public CreateBrandCommandHandlerTest()
    {
        _brandQueriesMock = new Mock<IBrandQueries>();
        _brandRepositoryMock = new Mock<IGenericRepository<Domain.Entity.Brand>>();

        _mockUnitOfWork.Setup(uow => uow.GetBaseRepo<Domain.Entity.Brand>()).Returns(_brandRepositoryMock.Object);

        _handler = new CreateBrandCommandHandler(_mapperConfig, _mockUnitOfWork.Object, _brandQueriesMock.Object, _cacheMock.Object, _cacheKeyConstants);
    }

    [Fact]
    public async Task Handle_ShouldAddBrand_WhenBrandDoesNotExist()
    {
        // Arrange
        var command = _fixture.Create<CreateBrandCommand>();
        var newBrand = _fixture.Create<Domain.Entity.Brand>();
        newBrand.Name = command.Name;

        _brandQueriesMock.Setup(q => q.CheckIsBrandExisted(command.Name)).ReturnsAsync(false);
        _brandRepositoryMock.Setup(repo => repo.Add(It.IsAny<Domain.Entity.Brand>())).Returns(newBrand);
        _mockUnitOfWork.Setup(uow => uow.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(command.Name);

        _brandQueriesMock.Verify(q => q.CheckIsBrandExisted(command.Name), Times.Once);
        _brandRepositoryMock.Verify(repo => repo.Add(It.IsAny<Domain.Entity.Brand>()), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowConflictException_WhenBrandExists()
    {
        // Arrange
        var command = _fixture.Create<CreateBrandCommand>();

        _brandQueriesMock.Setup(q => q.CheckIsBrandExisted(command.Name)).ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<ConflictException>(() => _handler.Handle(command, CancellationToken.None));

        _brandQueriesMock.Verify(q => q.CheckIsBrandExisted(command.Name), Times.Once);
        _brandRepositoryMock.Verify(repo => repo.Add(It.IsAny<Domain.Entity.Brand>()), Times.Never);
        _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldUpdateCache_WhenBrandIsAdded()
    {
        // Arrange
        var command = _fixture.Create<CreateBrandCommand>();
        var newBrand = _fixture.Create<Domain.Entity.Brand>();
        newBrand.Name = command.Name;

        _brandQueriesMock.Setup(q => q.CheckIsBrandExisted(command.Name)).ReturnsAsync(false);
        _brandRepositoryMock.Setup(repo => repo.Add(It.IsAny<Domain.Entity.Brand>())).Returns(newBrand);
        _mockUnitOfWork.Setup(uow => uow.CompleteAsync()).Returns(Task.CompletedTask);

        var cacheValues = _fixture.CreateMany<Domain.Entity.Brand>().ToList();
        _cacheMock.Setup(c => c.GetAsync<List<Domain.Entity.Brand>>(_cacheKeyConstants.BrandCacheKey)).ReturnsAsync(cacheValues);

        // Mocking DefaultCachePolicy and BuildOptions
        var cachePolicyMock = new Mock<CacheDefaults>();

        _cacheMock.Setup(c => c.DefaultCachePolicy).Returns(cachePolicyMock.Object);

        // Mock the Add method for IAppCache to avoid NullReferenceException
        _cacheMock.Setup(c => c.Add(It.IsAny<string>(), It.IsAny<List<Domain.Entity.Brand>>(), It.IsAny<MemoryCacheEntryOptions>()))
                  .Verifiable();
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(command.Name);

        _cacheMock.Verify(c => c.GetAsync<List<Domain.Entity.Brand>>(_cacheKeyConstants.BrandCacheKey), Times.Once);
        _cacheMock.Verify(c => c.Add(_cacheKeyConstants.BrandCacheKey, It.IsAny<List<Domain.Entity.Brand>>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Once);
    }
}
