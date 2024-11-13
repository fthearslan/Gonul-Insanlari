using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Notification
{
   public record NotificationSearchViewModel
    {
        public NotificationSearchViewModel(string searchInput)
        {
            SearchInput = searchInput;
        }
        public string SearchInput { get; set; }

        public string UserId {  get; set; }
       
        public List<string> UserPermissions { get; set; }  

    }
}
