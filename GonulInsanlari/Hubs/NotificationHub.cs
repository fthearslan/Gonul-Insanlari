using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SignalR;

namespace GonulInsanlari.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendNotificationAsync(string message)
        {

            await Clients.All.SendAsync("Receive",message);
        }
    }
}
