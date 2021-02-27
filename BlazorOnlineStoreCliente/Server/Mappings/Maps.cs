using AutoMapper;
using BlazorOnlineStoreCliente.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Server.Mappings
{
    public class Maps : Profile
    { 
        public Maps()
        {
            CreateMap<Address, Address>();
            CreateMap<BillingAddress, BillingAddress>();
            CreateMap<CardDetail, CardDetail>();
            CreateMap<Customer, Customer>();
            CreateMap<Order, Order>();
            CreateMap<OrderLineItem, OrderLineItem>();
            CreateMap<Product, Product>();                
        }
    }
}
