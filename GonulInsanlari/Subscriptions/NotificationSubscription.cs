using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TableDependency.SqlClient;
using ViewModelLayer.Models.Notification;
using ViewModelLayer.ViewModels.Notification;

namespace GonulInsanlari.Subscriptions
{
    public class NotificationSubscription : IDatabaseSubscription
    {

        IConfiguration _configuration;
        IHubContext<NotificationHub> _hubContext;

        public NotificationSubscription(IConfiguration configuration, IHubContext<NotificationHub> hub)
        {
            _configuration = configuration;
            _hubContext = hub;
        }

        SqlTableDependency<Notification> _tableDependency;

        public void Configure(string tableName)
        {

            _tableDependency = new SqlTableDependency<Notification>(_configuration.GetConnectionString("ConnectionString"), tableName);

            _tableDependency.OnChanged += async (o, e) =>
            {
                if (e.ChangeType == TableDependency.SqlClient.Base.Enums.ChangeType.Insert)
                {
                    Notification notification = e.Entity;

                    await _hubContext.Clients.All.SendAsync("Notify", new NotificationHubModel(notification.Title, notification.Content, notification.ResultType));

                }
            };

            _tableDependency.Start();
        }


        


        ~NotificationSubscription() => _tableDependency.Stop();

    }
}
