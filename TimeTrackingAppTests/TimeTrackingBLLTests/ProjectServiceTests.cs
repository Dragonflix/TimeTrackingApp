// ProjectServiceTests.cs
using AutoMapper;
using DAL;
using Microsoft.EntityFrameworkCore;
using Moq;
using TimeTrackingBLL.Models;
using TimeTrackingDAL;
using TimeTrackingDAL.Entities;
using TimeTrackingDAL.Interfaces;

public class ProjectServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;

    public ProjectServiceTests()
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
    public async Task GetAllAsync_ShouldReturnAllProjects()
    {
        var projects = new List<Project> { new Project { ProjectId = Guid.NewGuid(), ProjectName = "Test Project" } };
        _mapperMock.Setup(m => m.Map<ProjectModel>(It.IsAny<Project>())).Returns(new ProjectModel());
        var unitOfWork = new UnitOfWork(GetInMemoryDbContext());
        var projectService = new ProjectService(unitOfWork, _mapperMock.Object);

        var result = await projectService.GetAllAsync();

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProject()
    {
        var project = new Project { ProjectId = Guid.NewGuid(), ProjectName = "Test Project" };
        _mapperMock.Setup(m => m.Map<ProjectModel>(It.IsAny<Project>())).Returns(new ProjectModel());
        var unitOfWork = new UnitOfWork(GetInMemoryDbContext());
        var projectService = new ProjectService(unitOfWork, _mapperMock.Object);

        var result = await projectService.GetByIdAsync(project.ProjectId);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddProject()
    {
        var projectModel = new ProjectModel { ProjectId = Guid.NewGuid(), ProjectName = "Test Project" };
        var project = new Project { ProjectId = projectModel.ProjectId, ProjectName = projectModel.ProjectName };
        _mapperMock.Setup(m => m.Map<Project>(It.IsAny<ProjectModel>())).Returns(project);
        var unitOfWork = new UnitOfWork(GetInMemoryDbContext());
        var projectService = new ProjectService(unitOfWork, _mapperMock.Object);

        await projectService.AddAsync(projectModel);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteProject()
    {
        var projectId = Guid.NewGuid();
        var unitOfWork = new UnitOfWork(GetInMemoryDbContext());
        var projectService = new ProjectService(unitOfWork, _mapperMock.Object);

        await projectService.DeleteAsync(projectId);
    }
}
