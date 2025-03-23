using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTrackingDAL.Entities;
using TimeTrackingDAL.Repositories;
using Xunit;

public class ActivityTypeRepositoryTests
{
    private TimeTrackingDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<TimeTrackingDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new TimeTrackingDbContext(options);
    }

    [Fact]
    public async Task AddAsync_ShouldAddActivityType()
    {
        var context = GetInMemoryDbContext();
        var repository = new ActivityTypeRepository(context);
        var activityType = new ActivityType { ActivityTypeId = Guid.NewGuid(), ActivityName = "Test Activity" };

        await repository.AddAsync(activityType);
        await context.SaveChangesAsync();

        Assert.Single(context.ActivityTypes);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllActivityTypes()
    {
        var context = GetInMemoryDbContext();
        var repository = new ActivityTypeRepository(context);
        context.ActivityTypes.Add(new ActivityType { ActivityTypeId = Guid.NewGuid(), ActivityName = "Test Activity" });
        await context.SaveChangesAsync();

        var result = await repository.GetAllAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnActivityType()
    {
        var context = GetInMemoryDbContext();
        var repository = new ActivityTypeRepository(context);
        var id = Guid.NewGuid();
        context.ActivityTypes.Add(new ActivityType { ActivityTypeId = id, ActivityName = "Test Activity" });
        await context.SaveChangesAsync();

        var result = await repository.GetByIdAsync(id);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveActivityType()
    {
        var context = GetInMemoryDbContext();
        var repository = new ActivityTypeRepository(context);
        var id = Guid.NewGuid();
        context.ActivityTypes.Add(new ActivityType { ActivityTypeId = id, ActivityName = "Test Activity" });
        await context.SaveChangesAsync();

        await repository.DeleteAsync(id);
        await context.SaveChangesAsync();

        Assert.Empty(context.ActivityTypes);
    }
}
