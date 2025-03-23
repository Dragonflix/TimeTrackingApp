using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTrackingDAL.Entities;
using TimeTrackingDAL.Repositories;
using Xunit;

public class ProjectRepositoryTests
{
    private TimeTrackingDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<TimeTrackingDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new TimeTrackingDbContext(options);
    }

    [Fact]
    public async Task AddAsync_ShouldAddProject()
    {
        var context = GetInMemoryDbContext();
        var repository = new ProjectRepository(context);
        var project = new Project { ProjectId = Guid.NewGuid(), ProjectName = "Test Project" };

        await repository.AddAsync(project);
        await context.SaveChangesAsync();

        Assert.Single(context.Projects);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllProjects()
    {
        var context = GetInMemoryDbContext();
        var repository = new ProjectRepository(context);
        context.Projects.Add(new Project { ProjectId = Guid.NewGuid(), ProjectName = "Test Project" });
        await context.SaveChangesAsync();

        var result = await repository.GetAllAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProject()
    {
        var context = GetInMemoryDbContext();
        var repository = new ProjectRepository(context);
        var id = Guid.NewGuid();
        context.Projects.Add(new Project { ProjectId = id, ProjectName = "Test Project" });
        await context.SaveChangesAsync();

        var result = await repository.GetByIdAsync(id);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveProject()
    {
        var context = GetInMemoryDbContext();
        var repository = new ProjectRepository(context);
        var id = Guid.NewGuid();
        context.Projects.Add(new Project { ProjectId = id, ProjectName = "Test Project" });
        await context.SaveChangesAsync();

        await repository.DeleteAsync(id);
        await context.SaveChangesAsync();

        Assert.Empty(context.Projects);
    }
}
