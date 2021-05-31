using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace finalYearProject.Models
{
    public class Registration
    {
        [Required]
        public string firstname { get; set; }
        [Required]
        
        public string lastname { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("password",ErrorMessage ="Password and Confirmation password donot match.")]
        public string confirmPassword { get; set; }
    }
}
