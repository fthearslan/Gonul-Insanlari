using AutoMapper;
using EntityLayer.Concrete.Entities;
using ViewModelLayer.ViewModels.Assignment;
using ViewModelLayer.ViewModels.Notification;

namespace GonulInsanlari.Areas.Admin.AutoMapper.CustomResolvers
{
    public class NotificationTypeUrlResolver : IValueResolver<Notification, NotificationListViewModel, string>
    {
        public string Resolve(Notification source, NotificationListViewModel destination, string destMember, ResolutionContext context)
        {
            return source.Type switch
            {

                "Category" => "/categories/details",
                "Article" => "/articles/details",
                "Comment" => "/comments/details",
                "Contact" => "/contacts/details",
                _ => "/notifications/all"
            };

        }
    }
}
