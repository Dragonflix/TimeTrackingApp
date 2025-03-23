using DAL;
using Microsoft.EntityFrameworkCore;
using TimeTrackingDAL.Entities;
using TimeTrackingDAL.Interfaces;

namespace TimeTrackingDAL.Repositories
{
    public class TimeReportRepository: ITimeReportRepository
    {
        private readonly TimeTrackingDbContext _context;

        public TimeReportRepository(TimeTrackingDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TimeReport entity)
        {
            await _context.TimeReports.AddAsync(entity);
        }

        public async Task<IEnumerable<TimeReport>> GetByUserIdAsync(Guid id)
        {
            return await _context.TimeReports.Where(tr => tr.UserId == id).ToListAsync();
        }
    }
}
