using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace finalYearProject.Models
{
    public class Roles
    {
       
        public string id { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
