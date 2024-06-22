using AutoMapper;
using EntityLayer.Concrete.Entities;
using ViewModelLayer.ViewModels.Role;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class RoleProfile:Profile
    {

        public RoleProfile()
        {
            CreateMap<AppRole, ListRolesViewModel>();
        }
    }
}
