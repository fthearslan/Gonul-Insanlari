using AutoMapper;
using EntityLayer.Concrete.Entities;
using Humanizer;
using ViewModelLayer.ViewModels.Admin;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {

            CreateMap<AppUser, AdminProfileViewModel>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore())
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src=>src.Age));

            CreateMap<AppUser, AdminEditViewModel>();
            CreateMap<AdminEditViewModel, AppUser>().ForMember(x=>x.SecurityStamp,opt=>opt.Ignore());



        }
    }
}
