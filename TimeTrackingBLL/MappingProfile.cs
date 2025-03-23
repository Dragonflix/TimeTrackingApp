using AutoMapper;
using TimeTrackingBLL.Models;
using TimeTrackingDAL.Entities;

namespace TimeTrackingBLL
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<TimeReport, TimeReportModel>()
                    .ReverseMap();
            CreateMap<ActivityType, ActivityTypeModel>()
                    .ReverseMap();
            CreateMap<Project, ProjectModel>()
                    .ReverseMap();
        }
    }
}
