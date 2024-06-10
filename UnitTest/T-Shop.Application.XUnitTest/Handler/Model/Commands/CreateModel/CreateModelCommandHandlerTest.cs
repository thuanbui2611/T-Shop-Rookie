using AutoFixture;
using FluentAssertions;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using T_Shop.Application.Features.ModelProduct.Commands.CreateModelProduct;
using T_Shop.Application.Features.ModelProduct.Commands.CreateModelProductCommand;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.XUnitTest.Handler.Model.Commands.CreateModel;
public class CreateModelCommandHandlerTest : TestSetup
{
    private readonly Mock<IModelQueries> _modelQueriesMock;
    private readonly Mock<IBrandQueries> _brandQueriesMock;
    private readonly Mock<IGenericRepository<Domain.Entity.Model>> _modelRepositoryMock;
    private readonly CreateModelProductCommandHandler _handler;

    public CreateModelCommandHandlerTest()
    {
        _modelQueriesMock = new Mock<IModelQueries>();
        _brandQueriesMock = new Mock<IBrandQueries>();
        _modelRepositoryMock = new Mock<IGenericRepository<Domain.Entity.Model>>();
        _mockUnitOfWork.Setup(uow => uow.GetBaseRepo<Domain.Entity.Model>()).Returns(_modelRepositoryMock.Object);

        _handler = new CreateModelProductCommandHandler(
            _mapperConfig,
            _modelRepositoryMock.Object,
            _mockUnitOfWork.Object,
            _modelQueriesMock.Object,
            _brandQueriesMock.Object,
            _cacheMock.Object,
            _cacheKeyConstants
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowConflictException_WhenModelExistsForYear()
    {
        // Arrange

        var existingModels = _fixture.Create<List<Domain.Entity.Model>>().ToList();

        var request = _fixture.Build<CreateModelProductCommand>()
                            .With(b => b.Name, existingModels[0].Name)
                            .With(b => b.Year, existingModels[0].Year)
                            .Create();

        _modelQueriesMock.Setup(q => q.GetModelsByNameAsync(request.Name)).ReturnsAsync(existingModels);

        // Act & Assert
        await Assert.ThrowsAsync<ConflictException>(() => _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenBrandIsNotFound()
    {
        // Arrange
        var request = _fixture.Create<CreateModelProductCommand>();

        _modelQueriesMock.Setup(q => q.GetModelsByNameAsync(request.Name)).ReturnsAsync(new List<Domain.Entity.Model>());
        _brandQueriesMock.Setup(q => q.GetBrandByIdAsync(request.BrandID)).ReturnsAsync((Domain.Entity.Brand)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldCreateNewModel_WhenModelDoesNotExist()
    {
        var request = _fixture.Create<CreateModelProductCommand>();
        var brand = _fixture.Build<Domain.Entity.Brand>()
                            .With(b => b.Id, request.BrandID)
                            .Create();
        var newModel = _fixture.Build<Domain.Entity.Model>()
                               .With(m => m.Name, request.Name)
                               .With(m => m.Year, request.Year)
                               .With(m => m.BrandID, request.BrandID)
                               .Create();

        _modelQueriesMock.Setup(q => q.GetModelsByNameAsync(request.Name)).ReturnsAsync(new List<Domain.Entity.Model>());
        _brandQueriesMock.Setup(q => q.GetBrandByIdAsync(request.BrandID)).ReturnsAsync(brand);

        _modelRepositoryMock.Setup(repo => repo.Add(It.IsAny<Domain.Entity.Model>()));
        _mockUnitOfWork.Setup(uow => uow.CompleteAsync()).Returns(Task.CompletedTask);

        var cacheValues = new List<Domain.Entity.Model>();
        _cacheMock.Setup(c => c.GetAsync<List<Domain.Entity.Model>>(_cacheKeyConstants.ModelCacheKey)).ReturnsAsync(cacheValues);

        var cachePolicyMock = new Mock<CacheDefaults>();

        _cacheMock.Setup(c => c.DefaultCachePolicy).Returns(cachePolicyMock.Object);

        // Mock the Add method for IAppCache to avoid NullReferenceException
        _cacheMock.Setup(c => c.Add(It.IsAny<string>(), It.IsAny<List<Domain.Entity.Brand>>(), It.IsAny<MemoryCacheEntryOptions>()))
                  .Verifiable();
        _cacheMock.Setup(c => c.Remove(_cacheKeyConstants.BrandCacheKey)).Verifiable();

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);


        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);

        //_modelRepositoryMock.Verify(repo => repo.Add(It.IsAny<Domain.Entity.Model>()), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        _cacheMock.Verify(c => c.Add(_cacheKeyConstants.ModelCacheKey, It.IsAny<List<Domain.Entity.Model>>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Once);
        _cacheMock.Verify(c => c.Remove(_cacheKeyConstants.BrandCacheKey), Times.Once);
    }
}
