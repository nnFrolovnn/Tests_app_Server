using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tests_server_app.Models.ViewModels
{
    public class EditedUserVM
    {
        [Required]
        [MaxLength(250, ErrorMessage = "max length is 250")]
        public string Login { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "max length is 250")]
        public string PasswordHash { get; set; }

        [Required]
        [Compare(nameof(PasswordHash), ErrorMessage = "confirm password is not equal to origin")]
        public string ConfirmPasswordHash { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [MaxLength(250, ErrorMessage = "max length is 250")]
        public string Email { get; set; }
    }
}
