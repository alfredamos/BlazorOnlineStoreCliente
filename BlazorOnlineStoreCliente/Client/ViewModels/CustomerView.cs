using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Client.ViewModels
{
    public class CustomerView
    {
        public int CustomerID { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerAddress { get; set; }
        [Required]
        public string CustomerCity { get; set; }
        [Required]
        public string CustomerProvince { get; set; }
        [Required]
        public string CustomerCountry { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string CustomerPhoto { get; set; }
    }
}
