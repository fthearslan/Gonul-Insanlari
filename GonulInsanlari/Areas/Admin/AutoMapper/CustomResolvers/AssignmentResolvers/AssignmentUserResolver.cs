using AutoMapper;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment;

namespace GonulInsanlari.Areas.Admin.AutoMapper.CustomResolvers.AssignmentResolvers
{
    public class AssignmentUserResolver : IValueResolver<AssignmentCreateViewModel, Assignment, List<UserAssignment>>
    {
        public List<UserAssignment> Resolve(AssignmentCreateViewModel source, Assignment destination, List<UserAssignment> destMember, ResolutionContext context)
        {



            foreach (var userId in source.Users)
                destination.UserAssignments.Add(new UserAssignment()
                {
                    AssignmentId = destination.Id,
                    Assignment = destination,
                    UserId = userId,


                });

            return destination.UserAssignments;


        }
    }
}
