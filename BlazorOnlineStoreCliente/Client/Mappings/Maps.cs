﻿using AutoMapper;
using BlazorOnlineStoreCliente.Client.ViewModels;
using BlazorOnlineStoreCliente.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Client.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Customer, CustomerView>().ReverseMap();
            CreateMap<Order, OrderView>().ReverseMap();
            CreateMap<Product, ProductView>().ReverseMap();
        }
    }
}
