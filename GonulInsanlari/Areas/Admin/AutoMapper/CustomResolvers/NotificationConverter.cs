using AutoMapper;
using EntityLayer.Concrete.Entities;
using ViewModelLayer.Models.Tools;
using ViewModelLayer.ViewModels.Notification;

namespace GonulInsanlari.Areas.Admin.AutoMapper.CustomResolvers
{
    public class NotificationConverter : ITypeConverter<UserNotification, NotificationListViewModel>
    {
        public NotificationListViewModel Convert(UserNotification source, NotificationListViewModel dest, ResolutionContext context)
        {

            Notification notification = source.Notification;

            NotificationListViewModel destination = new NotificationListViewModel();

            destination.Url = notification.Type switch
            {

                "Category" => "/categories/details",
                "Article" => "/articles/details",
                "Comment" => "/comments/details",
                "Contact" => "/contacts/details",
                "Subscription" => "/subscriptions/list",
                _ => "/notifications/all"
            };

            destination.Value = notification.Value;
            destination.Title = notification.Title;
            destination.Content = notification.Content;
            destination.Created = notification.Created;
            destination.Symbol = notification.Symbol;
            destination.Type = notification.Type;
            destination.Id = notification.Id;
            destination.IsSeen = source.IsSeen;

            return destination;



        }
    }
}
