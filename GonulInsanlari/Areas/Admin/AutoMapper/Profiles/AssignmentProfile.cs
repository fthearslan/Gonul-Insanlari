using AutoMapper;
using EntityLayer.Entities;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class AssignmentProfile:Profile
    {
        public AssignmentProfile()
        {
            CreateMap<Assignment, AssignmentBarViewModel>();
        }
    }
}
