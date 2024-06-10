using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Admin
{
   public record AdminSearchLoginViewModel
    {

        public int Id { get; set; }
        public string Search { get; set; }
        
    }
}
