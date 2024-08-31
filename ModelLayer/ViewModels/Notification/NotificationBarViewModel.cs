using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Notification
{
    public record NotificationBarViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Symbol { get; set; }
        public string Title { get; set; }
        public int Value { get; set; }
        public DateTime Created { get; set; }


    }
}
