using AutoMapper;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment;

namespace GonulInsanlari.Areas.Admin.AutoMapper.CustomResolvers
{
    public class AssignmentUserListResolver : IValueResolver<Assignment, AssignmentDetailsViewModel, Dictionary<int,string>>
    {
        public Dictionary<int, string> Resolve(Assignment source, AssignmentDetailsViewModel destination, Dictionary<int, string> destMember, ResolutionContext context)
        {

            foreach (var obj in source.UserAssignments)
            {
                destination.Users.Add(obj.UserId,obj.User.Name);
            }

            return destination.Users;
        }
    }
}
