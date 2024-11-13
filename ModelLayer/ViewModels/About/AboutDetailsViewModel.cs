using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.About
{
    public class AboutDetailsViewModel
    {

        public AboutDetailsViewModel(int Id,string Title,string Details)
        {
            this.Id= Id;
            this.Title= Title;
            this.Details= Details;

        }


        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
