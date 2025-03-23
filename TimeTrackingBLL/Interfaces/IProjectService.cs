using TimeTrackingBLL.Models;

public interface IProjectService
{
    Task<IEnumerable<ProjectModel>> GetAllAsync();
    Task<ProjectModel> GetByIdAsync(Guid id);
    Task AddAsync(ProjectModel model);
    Task UpdateAsync(ProjectModel model);
    Task DeleteAsync(Guid id);
}
