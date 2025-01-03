﻿using AutoMapper;
using DataAccessLayer.Concrete.DTOs.Assignment;
using DataAccessLayer.Migrations;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.AutoMapper.CustomResolvers.AssignmentResolvers;
using ViewModelLayer.ViewModels.Assignment;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class AssignmentProfile : Profile
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
                .ForMember(a => a.UserAssignments, opt => opt.MapFrom<AssignmentUserResolver>());



            #endregion

            #region List

            CreateMap<AssignmentByProgressDto, AssignmentByProgressListViewModel>()
                .ForMember(dest => dest.Content,opt=>opt.MapFrom(src=>src.Content.Substring(0,200)));

            CreateMap<AssignmentListDto, AssignmentListViewModel>();


            #endregion

            #region Details 

            CreateMap<Assignment, AssignmentDetailsViewModel>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom<AssignmentUserListResolver>());



            #endregion
        }
    }
}
