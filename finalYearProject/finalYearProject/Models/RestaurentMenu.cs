using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace finalYearProject.Models
{
    public class RestaurentMenu
    {
        public string RestaurentName { get; set; }
        public string RestaurentID { get; set; }
        public int MenuID { get; set; }
        [Required(ErrorMessage ="Name of Item is Required")]
        public string NameOfItem { get; set; }
        [Required(ErrorMessage = "Price of Item is Required")]
        public string Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Quantity { get; set; }
        public IFormFile Photo2 { get; set; }
        public string PhotoPATH2 { get; set; }
        [Required]
        public string unit { get; set; }  
        public int SelectedQuantity { get; set; }
    }
}
