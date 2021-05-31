using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace finalYearProject.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string firstName { get; set; }
        public string LastName { get; set; }
    }
}
