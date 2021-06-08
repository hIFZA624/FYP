using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace finalYearProject.Models
{
    public class Users
    {
        public string id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
       
        public string email { get; set; }
        [Required]
        [EmailAddress]
        public string username { get; set; }
        public IList<string> Roles { get; set; }

    }
}
