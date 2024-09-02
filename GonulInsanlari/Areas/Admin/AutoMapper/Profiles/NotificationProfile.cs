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

            CreateMap<Notification, NotificationBarViewModel>();
            CreateMap<Notification, NotificationListViewModel>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom<NotificationTypeUrlResolver>());
                

            CreateMap<Notification, NotificationSearchResultViewModel>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom<NotificationTypeUrlResolver>())
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => GetDate.GetCreateDate(src.Created)));




        }
    }
}
