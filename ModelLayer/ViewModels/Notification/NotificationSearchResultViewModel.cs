using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Notification
{
    public record NotificationSearchResultViewModel:NotificationListViewModel
    {

        public string Date {  get; set; }



    }
}
