using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace finalYearProject.Models
{
    public class PUCITProducts
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string PhotoPath { get; set; }
        public IFormFile Photo { get; set; }
    }
}
