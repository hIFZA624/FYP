using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace finalYearProject.Models
{
    public class PUCITCafeCategory
    {
        public int Id { get; set; }
        public  string  CategoryName { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        public IFormFile Photo { get; set; }
    }
}
