// ActivityTypeServiceTests.cs
using AutoMapper;
using DAL;
using Microsoft.EntityFrameworkCore;
using Moq;
using TimeTrackingBLL.Models;
using TimeTrackingDAL;
using TimeTrackingDAL.Entities;
using TimeTrackingDAL.Interfaces;

public class ActivityTypeServiceTests
{
    private readonly Mock<IMapper> _mapperMock;

    public ActivityTypeServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
    }

    private TimeTrackingDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<TimeTrackingDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new TimeTrackingDbContext(options);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllActivityTypes()
    {
        var activityTypes = new List<ActivityType> { new ActivityType { ActivityTypeId = Guid.NewGuid(), ActivityName = "Test Activity" } };
        _mapperMock.Setup(m => m.Map<ActivityTypeModel>(It.IsAny<ActivityType>())).Returns(new ActivityTypeModel());
        var unitOfWork = new UnitOfWork(GetInMemoryDbContext());
        var activityTypeService = new ActivityTypeService(unitOfWork, _mapperMock.Object);

        var result = await activityTypeService.GetAllAsync();

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnActivityType()
    {
        var activityType = new ActivityType { ActivityTypeId = Guid.NewGuid(), ActivityName = "Test Activity" };
        _mapperMock.Setup(m => m.Map<ActivityTypeModel>(It.IsAny<ActivityType>())).Returns(new ActivityTypeModel());
        var unitOfWork = new UnitOfWork(GetInMemoryDbContext());
        var activityTypeService = new ActivityTypeService(unitOfWork, _mapperMock.Object);

        var result = await activityTypeService.GetByIdAsync(activityType.ActivityTypeId);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddActivityType()
    {
        var activityTypeModel = new ActivityTypeModel { ActivityTypeId = Guid.NewGuid(), ActivityName = "Test Activity" };
        var activityType = new ActivityType { ActivityTypeId = activityTypeModel.ActivityTypeId, ActivityName = activityTypeModel.ActivityName };
        _mapperMock.Setup(m => m.Map<ActivityType>(It.IsAny<ActivityTypeModel>())).Returns(activityType);
        var unitOfWork = new UnitOfWork(GetInMemoryDbContext());
        var activityTypeService = new ActivityTypeService(unitOfWork, _mapperMock.Object);

        await activityTypeService.AddAsync(activityTypeModel);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteActivityType()
    {
        var activityTypeId = Guid.NewGuid();
        var unitOfWork = new UnitOfWork(GetInMemoryDbContext());
        var activityTypeService = new ActivityTypeService(unitOfWork, _mapperMock.Object);

        await activityTypeService.DeleteAsync(activityTypeId);
    }
}
