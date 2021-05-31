using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace finalYearProject.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string currentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("newPassword", ErrorMessage = "Password and Confirmation password donot match.")]
        public string confirmedPassword { get; set; }
    }
}
