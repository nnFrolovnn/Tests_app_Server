using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Tests_server_app.Services.Authentication
{
    public class RoleHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            RoleRequirement requirement)
        {

            if (context.User.HasClaim(c => c.Type == ClaimTypes.Role))
            {
                var roleName = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value.ToLower();

                if (roleName == requirement.Role)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }

            }

            return Task.CompletedTask;
        }
    }
}
