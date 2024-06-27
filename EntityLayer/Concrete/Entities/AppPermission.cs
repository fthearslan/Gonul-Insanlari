using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Entities
{
    public class AppPermission
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Permission { get; set; } = null!;

    }
}
