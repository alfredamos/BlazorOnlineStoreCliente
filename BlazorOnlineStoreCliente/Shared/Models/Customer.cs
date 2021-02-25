using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Shared.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerProvince { get; set; }
        public string CustomerCountry { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CustomerPhoto { get; set; }
    }
}
