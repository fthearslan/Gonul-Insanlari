using AutoMapper;
using EntityLayer.Concrete.Entities;
using ViewModelLayer.ViewModels.Assignment;

namespace GonulInsanlari.Areas.Admin.AutoMapper.CustomResolvers.AssignmentResolvers
{
    public class AssignmentUserListResolver : IValueResolver<Assignment, AssignmentDetailsViewModel, List<(int, string, string)?>>
    {

        public List<(int, string, string)?> Resolve(Assignment source, AssignmentDetailsViewModel destination, List<(int, string, string)?> destMember, ResolutionContext context)
        {
            foreach (var obj in source.UserAssignments)
            {
                destination?.Users?.Add((obj.UserId, obj.User.UserName, obj.User.ImagePath));
            }

            return destination.Users;
        }
    }
}
