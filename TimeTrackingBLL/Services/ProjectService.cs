using AutoMapper;
using TimeTrackingDAL.Interfaces;
using TimeTrackingBLL.Models;
using TimeTrackingDAL.Entities;
using Microsoft.Extensions.Logging;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<IProjectService> _logger;

    public ProjectService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<IProjectService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<ProjectModel>> GetAllAsync()
    {
        _logger.LogInformation($"Fetching all Projects");
        var projects = await _unitOfWork.ProjectRepository.GetAllAsync();
        return projects.Select(_mapper.Map<ProjectModel>);
    }

    public async Task<ProjectModel> GetByIdAsync(Guid id)
    {
        _logger.LogInformation($"Fetching Project {id}");
        var project = await _unitOfWork.ProjectRepository.GetByIdAsync(id);
        return _mapper.Map<ProjectModel>(project);
    }

    public async Task AddAsync(ProjectModel model)
    {
        _logger.LogInformation($"Adding project");
        var project = _mapper.Map<Project>(model);
        await _unitOfWork.ProjectRepository.AddAsync(project);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(ProjectModel model)
    {
        _logger.LogInformation($"Updating Project {model.ProjectId}");
        var project = _mapper.Map<Project>(model);
        await _unitOfWork.ProjectRepository.UpdateAsync(project);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        _logger.LogInformation($"Deleting Project {id}");
        await _unitOfWork.ProjectRepository.DeleteAsync(id);
        await _unitOfWork.SaveAsync();
    }
}