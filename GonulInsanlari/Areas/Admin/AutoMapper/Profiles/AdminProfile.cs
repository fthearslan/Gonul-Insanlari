using AutoMapper;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Admin;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {

            CreateMap<AppUser, AdminProfileViewModel>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore());

        }
    }
}
