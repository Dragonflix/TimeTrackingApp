using DAL;
using Microsoft.EntityFrameworkCore;
using TimeTrackingDAL.Entities;
using TimeTrackingDAL.Interfaces;

public class ActivityTypeRepository : IActivityTypeRepository
{
    private readonly TimeTrackingDbContext _context;

    public ActivityTypeRepository(TimeTrackingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ActivityType>> GetAllAsync()
    {
        return await _context.ActivityTypes.ToListAsync();
    }

    public async Task<ActivityType> GetByIdAsync(Guid id)
    {
        return await _context.ActivityTypes.FirstOrDefaultAsync(p => p.ActivityTypeId == id);
    }

    public async Task AddAsync(ActivityType entity)
    {
        await _context.ActivityTypes.AddAsync(entity);
    }

    public async Task UpdateAsync(ActivityType entity)
    {
        _context.ActivityTypes.Update(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.ActivityTypes.FirstOrDefaultAsync(p => p.ActivityTypeId == id);
        if (entity != null)
        {
            _context.ActivityTypes.Remove(entity);
        }
    }
}
