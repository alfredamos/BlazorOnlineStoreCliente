using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Shared.Models
{
    public class OrderLineItem
    {
        public int OrderLineItemID { get; set; }        
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int OrderID { get; set; }        
        public Order Order { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
