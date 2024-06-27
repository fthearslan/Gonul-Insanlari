using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GonulInsanlari.Extensions.Admin
{
    public static class UserManagerExtensions
    {

        public static async Task<AppUser> GetUsersWithRoles(this UserManager<AppUser> userManager, string userId)
        {

           AppUser? user =  await userManager.FindByIdAsync(userId);

            if (user is null)
                return null;

            user.Roles = await userManager.GetRolesAsync(user);
            return user;

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


        public static async Task<IQueryable<AppUser>> GetUsersWithRolesAsync(this UserManager<AppUser> userManager, string roleName)
        {

            IList<AppUser> users = await userManager.GetUsersInRoleAsync(roleName);

            foreach (var user in users)
            {
                user.Roles = await userManager.GetRolesAsync(user);
            }

            return users.AsQueryable();

        }

        public static async Task<IQueryable<AppRole>> GetRolesWithPermissionsAsync(this UserManager<AppUser> userManager, ClaimsPrincipal _currentUser)
        {

            

            return null;
        }



    }
}
