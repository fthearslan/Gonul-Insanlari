using AutoMapper;
using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Extensions.Admin;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GonulInsanlari.Areas.Admin.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
           

            if (context.User.HasClaim(x => x.Type == "Permission" && x.Value == requirement.Permission))
            {

                context.Succeed(requirement);
               
                
                return Task.CompletedTask;
            }


            context.Fail();

            return Task.CompletedTask;
        }
    }
}
