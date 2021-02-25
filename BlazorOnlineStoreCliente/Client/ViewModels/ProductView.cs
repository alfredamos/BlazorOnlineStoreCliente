﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Client.ViewModels
{
    public class ProductView
    {
        public int ProductID { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string ImageLink { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
