using AutoMapper;
using EntityLayer.Concrete.Entities;
using ViewModelLayer.ViewModels.Notification;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class NotificationProfile : Profile
    {

        public NotificationProfile()
        {

            CreateMap<Notification, NotificationBarViewModel>();
        }
    }
}
