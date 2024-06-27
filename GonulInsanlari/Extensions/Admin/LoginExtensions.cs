using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GonulInsanlari.Extensions.Admin
{
    public static class LoginExtensions
    {

        public static async Task LogUserLoginAsync(this SignInManager<AppUser> signInManager, string userName, LoginType loginType)
        {

            using (var c = new Context())
            {

                var user = await c.Users.SingleOrDefaultAsync(x => x.UserName == userName);
                if (user is not null)
                {
                    string description = loginType switch
                    {
                        LoginType.Login => $"User named {user.UserName} logged in at {DateTime.Now}",
                        LoginType.Logout => $"User named {user.UserName} logged out at {DateTime.Now}",
                        _ => $"User named {user.UserName} logged in at {DateTime.Now}"
                    };


                    await c.UsersLogins.AddAsync(new()
                    {
                        User = user,
                        Type = loginType,
                        Description = description
                    });

                    await c.SaveChangesAsync();
                }

            }

        }


    }
}
