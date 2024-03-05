using AutoMapper;
using DataAccessLayer.Migrations;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.AutoMapper.CustomResolvers;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class AssignmentProfile:Profile
    {
        public AssignmentProfile()
        {
            #region Dashboard

            CreateMap<Assignment, AssignmentBarViewModel>()
                .ForMember(dest => dest.SubTasks, opt => opt.MapFrom(src => src.SubTasks.Count))
                .ForMember(dest => dest.SubTasksDone, opt => opt.MapFrom(src => src.SubTasks.Where(s => s.Progress == SubTasksProgress.Done).Count()));

            CreateMap<Assignment, AssignmentDashboardViewModel>();

            #endregion

            #region Add

            CreateMap<AssignmentCreateViewModel, Assignment>()
                .ForMember(a => a.SubTasks, opt => opt.MapFrom<SubTaskResolver>())
                .ForMember(a=>a.UserAssignments,opt=>opt.MapFrom<AssignmentUserResolver>());



            #endregion

            #region List

            CreateMap<Assignment, AssignmentInProgressListViewModel>()
                .ForMember(dest => dest.SubTasks, opt => opt.MapFrom(src => src.SubTasks.Count))
                .ForMember(dest => dest.SubTasksDone, opt => opt.MapFrom(src => src.SubTasks.Where(s => s.Progress == SubTasksProgress.Done).Count()))
                .ForMember(dest=>dest.UserImagePaths,opt=>opt.MapFrom<AssignmentUserImagePathResolver>());

            #endregion
        }
    }
}
