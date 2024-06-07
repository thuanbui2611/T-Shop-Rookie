using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using LazyCache;
using Moq;
using T_Shop.Application.Common.Constants;
using T_Shop.Application.Common.Mappings;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.XUnitTest;
public class TestSetup
{
    protected readonly Mock<IUnitOfWork> _mockUnitOfWork;
    protected readonly IFixture _fixture;
    protected readonly IMapper _mapperConfig;
    protected readonly Mock<IAppCache> _cacheMock;
    protected readonly CacheKeyConstants _cacheKeyConstants;
    public TestSetup()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });
        _mapperConfig = mappingConfig.CreateMapper();
        _cacheKeyConstants = new CacheKeyConstants();
        _cacheMock = new Mock<IAppCache>();
    }
}
