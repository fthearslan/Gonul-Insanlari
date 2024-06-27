using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ViewModelLayer.ViewModels.Permission;

namespace GonulInsanlari.Extensions.Admin
{
    public static class RoleManagerExtensions
    {

        public static async Task<List<string>> GetPermissionsAsync(this RoleManager<AppRole> _roleManager, int roleId)
        {

            AppRole? role = await _roleManager.Roles.SingleOrDefaultAsync(x => x.Id == roleId);

            if (role is null)
                return null;

            IList<Claim> claims = await _roleManager.GetClaimsAsync(role);

            if (claims is null)
                return null;

            return claims
                            .Where(x => x.Type == "Permission")
                            .Select(x => x.Value)
                            .ToList();

        }

        public static bool AddPermission(this RoleManager<AppRole> _roleManager, List<string> permissions, int roleId)
        {

            if (permissions is null)
                return false;
            List<IdentityRoleClaim<int>> claims = new();

            foreach (string perm in permissions)
                claims.Add(new() { ClaimType = "Permission", ClaimValue = perm, RoleId = roleId });

            using (var c = new Context())
            {
                c.RoleClaims.AddRange(claims);

                if (claims.Count == c.SaveChanges())
                    return true;

            }

            return false;

        }

        public static bool RemovePermission(this RoleManager<AppRole> _roleManager, List<string> permissions, int roleId)
        {

            using (var c = new Context())
            {
                permissions.ForEach(perm =>
                {

                    var permissionRemoved = c.RoleClaims.SingleOrDefault(x => x.RoleId == roleId && x.ClaimValue == perm);

                    if (permissionRemoved is not null)
                        c.RoleClaims.Remove(permissionRemoved);
                });



                if (permissions.Count == c.SaveChanges())
                    return true;

            }

            return false;


        }



    }
}
