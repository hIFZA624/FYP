using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace finalYearProject.Models
{
    public class EditRole
    {
        
        public string Id { get; set; }
        [Required]
        public string RoleName { get; set; }
        public List<string> users { get; set; }
    }
}
