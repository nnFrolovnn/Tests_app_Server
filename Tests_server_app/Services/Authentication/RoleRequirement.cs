using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Tests_server_app.Services.Authentication
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        protected internal string Role { get; set; }

        /// <summary>
        /// Case ignored
        /// </summary>
        /// <param name="role"></param>
        public RoleRequirement(string role)
        {
            Role = role.ToLower();
        }
    }
}
