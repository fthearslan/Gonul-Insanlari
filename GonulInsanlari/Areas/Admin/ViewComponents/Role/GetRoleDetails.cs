using EntityLayer.Concrete.Entities;
using GonulInsanlari.Extensions.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using ViewModelLayer.ViewModels.Role;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Role
{
    public class GetRoleDetails : ViewComponent
    {

        private readonly UserManager<AppUser> _userManager;
        public GetRoleDetails(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke(string roleName,int roleId,string desc)
        {

            var users = _userManager.GetUsersWithRolesAsync(roleName).Result;

            List<ListUsersByRoleViewModel> model = new();
            
            foreach (var user in users)
                model.Add(new() { Id = user.Id, UserName = user.UserName, ImagePath=user.ImagePath, Status=user.Status,Roles = user.Roles.ToList() });

            ViewData["RoleName"] = roleName;
            ViewData["RoleDesc"] = desc;
            ViewData["RoleId"] = roleId;

            return View(model);
        }
    }
}
