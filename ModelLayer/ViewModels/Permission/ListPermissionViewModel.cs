using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Permission
{
    public record ListPermissionViewModel
    {
        public string Type { get; set; } = null!;
        public string Permission { get; set; } = null!;

        public bool Exists { get; set; }= false;

    }


    public record PermissionViewModel
    {

        public string Type { get; set; } = null!;

        public List<string>? Permissions { get; set; }

    }
}
