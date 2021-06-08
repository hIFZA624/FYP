using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace finalYearProject.Models
{
    public class Restaurent
    {
        [Required(ErrorMessage = "Please enter id in email format")]
        [EmailAddress]
        public string RestaurentID { get; set; }
        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter Name")]
        [StringLength(50)]
        public string NameOfRestaurants { get; set; }
        [Required(ErrorMessage = "Please enter Address")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please enter Phone No")]
        public string PhoneNo { get; set; }

        public string OpenUntil { get; set; }

        public string DeliveryCharges { get; set; }

        public string PhotoPATH { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
