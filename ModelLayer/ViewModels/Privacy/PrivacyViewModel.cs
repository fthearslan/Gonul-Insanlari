using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Privacy
{
   public record PrivacyViewModel
    {
        public PrivacyViewModel(int Id, string Title,string Content)
        {
            this.Id = Id;
            this.Title = Title;
            this.Content = Content;
        }
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public DateTime LastUpdated { get; set; }

    }
}
