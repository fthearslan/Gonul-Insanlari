using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SignalR;
using ViewModelLayer.Models.Notification;
using ViewModelLayer.ViewModels.Notification;

namespace GonulInsanlari.Hubs
{
    public class NotificationHub : Hub
    {
      
        public async Task NotifyAsync(NotificationHubModel notification)
        {

            await Clients.All.SendAsync("Notify", notification);
       
        }


        public async Task NotifyError(string title,string exception)
        {

            await Clients.All.SendAsync("NotifyError",title,exception);
        }
    }
}
