using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace finalYearProject.Models
{
    public class MyDBContext:IdentityDbContext<ApplicationUser>
    {
        public MyDBContext(DbContextOptions<MyDBContext> options):base(options)
        {

        }
      

    }
}
