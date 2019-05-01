using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests_server_app.Models.DBModels;

namespace Tests_server_app.Extensions
{
    public static class StringExtensions
    {
        public static RolesName ToRole(this string roleName)
        {
            string tempRole = roleName.Trim();
            tempRole =
                tempRole.Substring(0, 1).ToUpper() +
                tempRole.Substring(1).ToLower();

            foreach(var role in Enum.GetValues(typeof(RolesName)).Cast<RolesName>())
            {
                if(nameof(role) == tempRole)
                {
                    return role;
                }
            }

            throw new Exception("No such value in Enum");
        }

    }
}
