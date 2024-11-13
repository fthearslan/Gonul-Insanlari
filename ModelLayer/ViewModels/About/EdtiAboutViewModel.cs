using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.About
{
    public record EdtiAboutViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }

    }
}
