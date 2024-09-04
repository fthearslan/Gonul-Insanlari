using AutoMapper;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.AutoMapper.CustomResolvers;
using Org.BouncyCastle.Crypto.Tls;
using ViewModelLayer.Models.Tools;
using ViewModelLayer.ViewModels.Notification;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class NotificationProfile : Profile
    {

        public NotificationProfile()
        {

                

            CreateMap<UserNotification, NotificationListViewModel>().ConvertUsing<NotificationConverter>();


        }
    }
}
