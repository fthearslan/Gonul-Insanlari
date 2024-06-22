using EntityLayer.Abstract;
using GonulInsanlari.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Text;

namespace GonulInsanlari.Areas.Admin.Authorization
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(PermissionType permissionType,Permission permission) : base(String.Join(".",permissionType.ToString(), permission.ToString()))
        {


        }
    }
}
