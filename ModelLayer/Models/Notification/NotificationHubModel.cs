using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.Models.Notification
{
    public record NotificationHubModel
    {
        public NotificationHubModel(string title,string content, NotificationResultType resultType)
        {
            Title = title;
            Content=content;
            ResultType = resultType.ToString();

        }

        public string Title { get; set; }   

        public string Content { get; set; }

        public string ResultType { get; set; }

     

    }
}
