using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Privacy
{
    public record EditPrivacyViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

    }
}
