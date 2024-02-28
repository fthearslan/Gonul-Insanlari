﻿using AutoMapper;
using DataAccessLayer.Migrations;
using EntityLayer.Entities;
using GonulInsanlari.Areas.Admin.AutoMapper.CustomResolvers;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class AssignmentProfile:Profile
    {
        public AssignmentProfile()
        {
            #region Dashboard

            CreateMap<Assignment, AssignmentBarViewModel>();
            CreateMap<Assignment, AssignmentDashboardViewModel>();

            #endregion

            #region Add

            CreateMap<AssignmentCreateViewModel, Assignment>()
                .ForMember(a => a.SubTasks, opt => opt.MapFrom<SubTaskResolver>())
                .ForMember(a=>a.UserAssignments,opt=>opt.MapFrom<AssignmentUserResolver>());



            #endregion

        }
    }
}