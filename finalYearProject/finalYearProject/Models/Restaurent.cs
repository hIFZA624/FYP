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
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Must be at least 4 characters long.")]
        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter Name")]
        [StringLength(50)]
        public string NameOfRestaurants { get; set; }
        [Required(ErrorMessage = "Please enter Address")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Must be at least 10 characters long.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please enter Phone No")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public string PhoneNo { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public string OpenUntil { get; set; }
        
        [Required]
        public string DeliveryCharges { get; set; }

        public string PhotoPATH { get; set; }
      
        public IFormFile Photo { get; set; }
    }
}
