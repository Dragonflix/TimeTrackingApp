using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTrackingDAL.Entities;
using TimeTrackingDAL.Repositories;
using Xunit;

public class TimeReportRepositoryTests
{
    private TimeTrackingDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<TimeTrackingDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new TimeTrackingDbContext(options);
    }

    [Fact]
    public async Task AddAsync_ShouldAddTimeReport()
    {
        var context = GetInMemoryDbContext();
        var repository = new TimeReportRepository(context);
        var timeReport = new TimeReport { UserId = Guid.NewGuid(), StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1), ActivityDescription = "test" };

        await repository.AddAsync(timeReport);
        await context.SaveChangesAsync();

        Assert.Single(context.TimeReports);
    }

    [Fact]
    public async Task GetByUserIdAndDateAsync_ShouldReturnTimeReports()
    {
        var context = GetInMemoryDbContext();
        var repository = new TimeReportRepository(context);
        var userId = Guid.NewGuid();
        var date = DateOnly.FromDateTime(DateTime.Now);
        context.TimeReports.Add(new TimeReport { UserId = userId, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1), ActivityDescription = "test" });
        await context.SaveChangesAsync();

        var result = await repository.GetByUserIdAndDateAsync(userId, date);

        Assert.Single(result);
    }

    [Fact]
    public async Task GetByUserIdAndWeekAsync_ShouldReturnTimeReports()
    {
        var context = GetInMemoryDbContext();
        var repository = new TimeReportRepository(context);
        var userId = Guid.NewGuid();
        var date = DateOnly.FromDateTime(DateTime.Now);
        context.TimeReports.Add(new TimeReport { UserId = userId, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1), ActivityDescription = "test" });
        await context.SaveChangesAsync();

        var result = await repository.GetByUserIdAndWeekAsync(userId, 1);

        Assert.Single(result);
    }
}
