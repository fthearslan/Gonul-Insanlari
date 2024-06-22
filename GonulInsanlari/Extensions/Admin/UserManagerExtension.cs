using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GonulInsanlari.Extensions.Admin
{
    public static class UserManagerExtension
    {

        public static async Task<AppUser> GetUsersWithRoles(this UserManager<AppUser> userManager, AppUser user)
        {

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

            //AppUser user = await userManager.GetUserAsync(_currentUser);

            //if (user is not null)
            //{
            //    using (var c = new Context())
            //    {
            //        c.Roles.
            //            Include(x=>x.Name);
            //        //first create permission table 

            //    }


            //}

            return null;
        }



    }
}
