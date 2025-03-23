using DAL;
using Microsoft.EntityFrameworkCore;
using TimeTrackingDAL.Entities;
using TimeTrackingDAL.Interfaces;

public class ProjectRepository: IProjectRepository
{
    private readonly TimeTrackingDbContext _context;

    public ProjectRepository(TimeTrackingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task<Project> GetByIdAsync(Guid id)
    {
        return await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);
    }

    public async Task AddAsync(Project entity)
    {
        await _context.Projects.AddAsync(entity);
    }

    public async Task UpdateAsync(Project entity)
    {
        _context.Projects.Update(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);
        if (entity != null)
        {
            _context.Projects.Remove(entity);
        }
    }
}
