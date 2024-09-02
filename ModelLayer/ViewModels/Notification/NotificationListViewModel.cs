using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Notification
{
    public record NotificationListViewModel
    {

        public int Id { get; set; }
        public string Type { get; set; }
        public string Symbol { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public int Value { get; set; }
        public DateTime Created { get; set; }




    }
}
