using GonulInsanlari.Areas.Admin.ViewComponents.Role;
using Microsoft.AspNetCore.Authorization;

namespace GonulInsanlari.Areas.Admin.Authorization
{
    public class PermissionRequirement:IAuthorizationRequirement
    {
      
        public string Permission { get; set; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}
