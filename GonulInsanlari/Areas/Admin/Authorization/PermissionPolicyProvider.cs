﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace GonulInsanlari.Areas.Admin.Authorization
{
    public class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
        }

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {

            AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);

            if (policy is not null)
                return policy;


            return new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(policyName))
                .Build();
            



        }

      
    }
}
