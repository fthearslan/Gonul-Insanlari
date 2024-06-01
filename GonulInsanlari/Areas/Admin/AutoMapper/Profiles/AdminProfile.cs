using AutoMapper;
using EntityLayer.Concrete.Entities;
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
              

        }
    }
}
