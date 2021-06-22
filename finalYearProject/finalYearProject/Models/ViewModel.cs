using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace finalYearProject.Models
{
    public class viewmodel
    {
        public IEnumerable<CartAndOrder> Cart { get; set; }
       public Receipt receipt { get; set; }
    }
}
