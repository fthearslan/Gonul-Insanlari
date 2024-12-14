using EntityLayer.Concrete.Entities;
using GonulInsanlari.Extensions.Admin;
using Microsoft.AspNetCore.Identity;
using static Org.BouncyCastle.Math.EC.ECCurve;
using ViewModelLayer.ViewModels.Permission;

namespace GonulInsanlari.Middlewares
{
    public static class UserConfigurationMiddleware
    {

        public static void UseUserConfiguration(this IApplicationBuilder builder)
        {

            IConfiguration _config = builder.ApplicationServices.GetRequiredService<IConfiguration>();

            IServiceScope serviceScope = builder.ApplicationServices.CreateScope();

            UserManager<AppUser> userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            RoleManager<AppRole> roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

            if (!roleManager.Roles.Any(x => x.Name == "Admin"))
            {

                AppRole admin = new()
                {
                    Name = "Admin",
                    Description = "Created by system."


                };

                roleManager.CreateAsync(admin).Wait();

                List<PermissionViewModel>? appPermissions = _config
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
                    UserName = "System Admin",
                    Name = "System",
                    Surname = "Administrator",
                    Email = "ginsanlari@gmail.com",
                    EmailConfirmed = true,
                    Registered = DateTime.Now,
                    SocialMediaAccount = "ginsanlari@gmail.com",
                    Age = 18,
                    Status = true,
                };

                userManager.CreateAsync(admin)
               .Wait();

                userManager.AddToRoleAsync(admin, "Admin");

            }


        }
    }
}
