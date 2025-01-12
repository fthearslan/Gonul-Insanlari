﻿using AutoMapper;
using EntityLayer.Concrete.Entities;
using ViewModelLayer.ViewModels.Assignment;

namespace GonulInsanlari.Areas.Admin.AutoMapper.CustomResolvers.AssignmentResolvers
{
    public record AssignmentUserImagePathResolver : IValueResolver<Assignment, AssignmentByProgressListViewModel, List<string>>
    {
        public List<string> Resolve(Assignment source, AssignmentByProgressListViewModel destination, List<string> destMember, ResolutionContext context)
        {

            List<string> userImagePaths = new();

            foreach (var userAssignment in source.UserAssignments)
            {
                userImagePaths.Add(userAssignment.User.ImagePath);
            }

            return userImagePaths;


        }
    }
}
