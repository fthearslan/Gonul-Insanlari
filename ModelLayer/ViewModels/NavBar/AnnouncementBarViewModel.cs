using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.NavBar
{
    public record AnnouncementBarViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        string? _details;
        public string Details
        {

            get
            {
                return _details;
            }
            set
            {
                _details = value;
            }

        }

        public DateTime Created { get; set; }
    }
}
