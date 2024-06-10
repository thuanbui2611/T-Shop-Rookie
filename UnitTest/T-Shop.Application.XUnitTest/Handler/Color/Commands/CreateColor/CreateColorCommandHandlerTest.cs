using AutoFixture;
using FluentAssertions;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Features.Color.Commands.CreateColor;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.XUnitTest.Handler.Color.Commands.CreateColor;
public class CreateColorCommandHandlerTest : TestSetup
{
    private readonly Mock<IColorQueries> _colorQueriesMock;
    private readonly Mock<IGenericRepository<Domain.Entity.Color>> _colorRepositoryMock;
    private readonly CreateColorCommandHandler _handler;

    public CreateColorCommandHandlerTest()
    {
        _colorQueriesMock = new Mock<IColorQueries>();
        _colorRepositoryMock = new Mock<IGenericRepository<Domain.Entity.Color>>();

        _mockUnitOfWork.Setup(uow => uow.GetBaseRepo<Domain.Entity.Color>()).Returns(_colorRepositoryMock.Object);

        _handler = new CreateColorCommandHandler(_mapperConfig, _mockUnitOfWork.Object, _colorQueriesMock.Object, _cacheMock.Object, _cacheKeyConstants);
    }

    [Fact]
    public async Task Handle_ShouldAddColor_WhenColorDoesNotExist()
    {
        // Arrange
        var command = _fixture.Create<CreateColorCommand>();
        var newColor = _fixture.Create<Domain.Entity.Color>();
        newColor.Name = command.Name;

        _colorQueriesMock.Setup(q => q.CheckIsColorExisted(command.Name)).ReturnsAsync(false);
        _colorRepositoryMock.Setup(repo => repo.Add(It.IsAny<Domain.Entity.Color>())).Returns(newColor);
        _mockUnitOfWork.Setup(uow => uow.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(command.Name);

        _colorQueriesMock.Verify(q => q.CheckIsColorExisted(command.Name), Times.Once);
        _colorRepositoryMock.Verify(repo => repo.Add(It.IsAny<Domain.Entity.Color>()), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowConflictException_WhenColorExists()
    {
        // Arrange
        var command = _fixture.Create<CreateColorCommand>();

        _colorQueriesMock.Setup(q => q.CheckIsColorExisted(command.Name)).ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<ConflictException>(() => _handler.Handle(command, CancellationToken.None));

        _colorQueriesMock.Verify(q => q.CheckIsColorExisted(command.Name), Times.Once);
        _colorRepositoryMock.Verify(repo => repo.Add(It.IsAny<Domain.Entity.Color>()), Times.Never);
        _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldUpdateCache_WhenColorIsAdded()
    {
        // Arrange
        var command = _fixture.Create<CreateColorCommand>();
        var newColor = _fixture.Create<Domain.Entity.Color>();
        newColor.Name = command.Name;

        _colorQueriesMock.Setup(q => q.CheckIsColorExisted(command.Name)).ReturnsAsync(false);
        _colorRepositoryMock.Setup(repo => repo.Add(It.IsAny<Domain.Entity.Color>())).Returns(newColor);
        _mockUnitOfWork.Setup(uow => uow.CompleteAsync()).Returns(Task.CompletedTask);

        var cacheValues = _fixture.CreateMany<Domain.Entity.Color>().ToList();
        _cacheMock.Setup(c => c.GetAsync<List<Domain.Entity.Color>>(_cacheKeyConstants.ColorCacheKey)).ReturnsAsync(cacheValues);

        // Mocking DefaultCachePolicy and BuildOptions
        var cachePolicyMock = new Mock<CacheDefaults>();

        _cacheMock.Setup(c => c.DefaultCachePolicy).Returns(cachePolicyMock.Object);

        // Mock the Add method for IAppCache to avoid NullReferenceException
        _cacheMock.Setup(c => c.Add(It.IsAny<string>(), It.IsAny<List<Domain.Entity.Color>>(), It.IsAny<MemoryCacheEntryOptions>()))
                  .Verifiable();
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(command.Name);

        _cacheMock.Verify(c => c.GetAsync<List<Domain.Entity.Color>>(_cacheKeyConstants.ColorCacheKey), Times.Once);
        _cacheMock.Verify(c => c.Add(_cacheKeyConstants.ColorCacheKey, It.IsAny<List<Domain.Entity.Color>>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Once);
    }
}
