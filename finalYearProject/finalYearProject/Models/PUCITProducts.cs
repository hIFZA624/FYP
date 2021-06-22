using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace finalYearProject.Models
{
    public class PUCITProducts
    {
        public int Id { get; set; }
        [Required]
        public string Category { get; set; }
        [Required(ErrorMessage ="Name Field is required attribute")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Price Field is required attribute")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Price { get; set; }
       [Required(ErrorMessage = "Quantity Field is required attribute")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Quantity { get; set; }
        public string PhotoPath { get; set; }
       
        public IFormFile Photo { get; set; }
    }
}
