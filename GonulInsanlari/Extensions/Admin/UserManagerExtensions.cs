using BussinessLayer.Concrete.Managers;
using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Org.BouncyCastle.Math.EC.ECCurve;
using ViewModelLayer.ViewModels.Permission;

namespace GonulInsanlari.Extensions.Admin
{
    public static class UserManagerExtensions
    {


        public static async Task<AppUser> GetByIdAsync(this UserManager<AppUser> _userManager, int userId)
        {
            return await _userManager.Users
                .SingleOrDefaultAsync(x => x.Id == userId);

        }
        public static async Task<AppUser> GetUsersWithRoles(this UserManager<AppUser> userManager, string userId)
        {

            AppUser? user = await userManager.FindByIdAsync(userId);

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

        public static List<string> GetUserPermissions(this UserManager<AppUser> _userManager, ClaimsPrincipal _currentUser)
        {

            return _currentUser.Claims.
                    Where(x => x.Type == "Permission")
                    .Select(x => x.Value)
            .ToList();
        }

        public static void ConfigureDefaultUser(this UserManager<AppUser> userManager,RoleManager<AppRole> roleManager,IConfiguration configuration)
        {
          


            if (!roleManager.Roles.Any(x => x.Name == "Admin"))
            {

                AppRole admin = new()
                {
                    Name = "Admin",
                    Description = "Created by system."
                };

                roleManager.CreateAsync(admin)
                    .Wait();

                List<PermissionViewModel>? appPermissions = configuration
                    .GetSection("AppPermissions")
                    .Get<List<PermissionViewModel>>();

                List<string> permissions = new();


                appPermissions?.ForEach(appPermission =>
                {
                    if (appPermission.Permissions is not null)
                        permissions.AddRange(appPermission.Permissions);

                });

                roleManager.AddPermission(permissions, admin.Id);

            }


            if (userManager.Users.Count(x => x.Status == true) == 0)
            {

                AppUser admin = new()
                {
                    UserName = "Admin",
                    Name = "System",
                    Surname = "Administrator",
                    Email = "ginsanlari@gmail.com",
                    EmailConfirmed = true,
                    Registered = DateTime.Now,
                    SocialMediaAccount = "ginsanlari@gmail.com",
                    ImagePath= "profile.jpg",
                    Age = 18,
                    Status = true,
                    PhoneNumber="Not added yet."
                };

                userManager.CreateAsync(admin,"23592359Aa@")
               .Wait();

                userManager.AddToRoleAsync(admin, "Admin").
                    Wait();

            }


        }

    }
}
