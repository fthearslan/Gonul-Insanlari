using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GonulInsanlari.Extensions.Admin
{
    public static class UserManagerExtension
    {

        public static async Task GetUsersWithRoles(this UserManager<AppUser> userManager, AppUser user)
        {

            user.Roles = await userManager.GetRolesAsync(user);
      

        }
        public static async Task<IQueryable<AppUser>> GetUsersWithRolesAsync(this UserManager<AppUser> userManager)
        {

            var users = userManager.Users.AsQueryable();

            foreach (var user in await users.ToListAsync())
            {
                user.Roles = await userManager.GetRolesAsync(user);
            }
           
            return users;

        }


    }
}
