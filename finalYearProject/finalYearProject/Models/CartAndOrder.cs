using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace finalYearProject.Models
{
    public class CartAndOrder
    {
        public int CartID { get; set; }
        [DataType(DataType.EmailAddress)]
        public string RestaurentID { get; set; }
        public int MenuID { get; set; }
        [DataType(DataType.EmailAddress)]
        public string UserID { get; set; }
        public string NameOfItem { get; set; }
        public string Price { get; set; }
        public string OriginalPrice { get; set; }
        public int Quantity { get; set; }
        public int ReceiptID { get; set; }
    }
}
