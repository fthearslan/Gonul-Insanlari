using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ViewModelLayer.ViewModels.Role;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Role
{
    public class RoleGetUsersToAdd : ViewComponent
    {

        private readonly UserManager<AppUser> _userManager;

        public RoleGetUsersToAdd(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public  IViewComponentResult Invoke()
        {

            string? roleName = ViewData["RoleName"]?.ToString();

            if (roleName is null)
                return View(null);

            var users = _userManager
                 .Users.
                 ToList();

            List<AddUserToRoleViewModel> model = new();

            foreach (AppUser user in users)
            {
                if (!_userManager.IsInRoleAsync(user, roleName).Result)
                    model.Add(new() { Id = user.Id, Username = user.UserName, ImagePath = user.ImagePath, });

                continue;

            }

            return View(model);
        }
    }
}
