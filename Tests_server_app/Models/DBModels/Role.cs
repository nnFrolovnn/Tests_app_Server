using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tests_server_app.Models.DBModels
{
    public enum RolesName
    {
        User, Admin, Boss
    }

    public enum RolesPermissions
    {
        All, CreateTests, DeleteTests, LikeSmth, AllExceptDeleteAndLikeTests
    }

    public class Role
    {
        [Key]
        public long RoleId { get; set; }

        public RolesName RoleName { get; set; }

        public RolesPermissions Permissions { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
