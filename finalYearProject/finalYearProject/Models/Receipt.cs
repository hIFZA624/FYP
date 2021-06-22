using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace finalYearProject.Models
{
    public class Receipt
    {
        public int ReceiptID { get; set; }
        [DataType(DataType.EmailAddress)]
        public string UserID { get; set; }
        [DataType(DataType.EmailAddress)]
        public string RestaurentID { get; set; }
        public string TotalAmount { get; set; }
        public int NoOfItems { get; set; }
        [Required]
        public string UserTime { get; set; }
        public DateTime OrderTime { get; set; }
        public int OrderAccept { get; set; }
        public int OrderReceived { get; set; }
    }
}
