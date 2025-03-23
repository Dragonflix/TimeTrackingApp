using TimeTrackingDAL.Entities;

namespace TimeTrackingDAL.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync();

        Task<Project> GetByIdAsync(Guid id);

        Task AddAsync(Project entity);

        Task UpdateAsync(Project entity);

        Task DeleteAsync(Guid id);
    }
}
