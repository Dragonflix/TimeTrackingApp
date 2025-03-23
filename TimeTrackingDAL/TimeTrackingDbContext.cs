using Microsoft.EntityFrameworkCore;
using TimeTrackingDAL.Entities;
using System.Data;

namespace DAL
{
    public class TimeTrackingDbContext : DbContext
    {
        public TimeTrackingDbContext()
        {
        }

        public TimeTrackingDbContext(DbContextOptions<TimeTrackingDbContext> options)
            : base(options)
        {
        }

        public DbSet<TimeReport> TimeReports { get; set; }

        public DbSet<ActivityType> ActivityTypes { get; set; }

        public DbSet<Project> Projects { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeReport>()
                .HasKey(e => e.TimeReportId);
            modelBuilder.Entity<ActivityType>()
                .HasKey(e => e.ActivityTypeId);
            modelBuilder.Entity<ActivityType>()
                .HasIndex(e => e.ActivityName)
                .IsUnique();
            modelBuilder.Entity<Project>()
                .HasKey(e => e.ProjectId);
            modelBuilder.Entity<Project>()
                .HasIndex(e => e.ProjectName)
                .IsUnique();
        }
    }
}
