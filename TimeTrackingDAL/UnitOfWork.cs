using DAL;
using TimeTrackingDAL.Interfaces;
using TimeTrackingDAL.Repositories;

namespace TimeTrackingDAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TimeTrackingDbContext _context;

        public ITimeReportRepository TimeReportRepository { get; }

        public IActivityTypeRepository ActivityTypeRepository { get; }

        public IProjectRepository ProjectRepository { get; }

        public UnitOfWork(TimeTrackingDbContext context)
        {
            _context = context;
            TimeReportRepository = new TimeReportRepository(_context);
            ActivityTypeRepository = new ActivityTypeRepository(_context);
            ProjectRepository = new ProjectRepository(_context);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
