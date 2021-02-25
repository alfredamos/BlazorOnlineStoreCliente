using AutoMapper;
using BlazorOnlineStoreCliente.Client.Contracts;
using BlazorOnlineStoreCliente.Client.ViewModels;
using BlazorOnlineStoreCliente.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Client.Pages.Orders
{
    public class PlaceOrderBase : ComponentBase
    {
        [Inject]
        public IOrderService OrderService { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public string OrderNumber { get; set; }

        public List<Customer> CustomersT { get; set; } = new List<Customer>();

        public List<CustomerView> Customers { get; set; } = new List<CustomerView>();

        public List<OrderView> Orders { get; set; } = new List<OrderView>();

        public List<Order> OrdersT { get; set; } = new List<Order>();

        public List<ProductView> Products { get; set; } = new List<ProductView>();

        public List<Product> ProductsT { get; set; } = new List<Product>();

        public double SubTotal { get; set; } = 0.0;

        public double Total { get; set; } = 0.0;

        protected override async Task OnInitializedAsync()
        {
            OrdersT = (await OrderService.Search(OrderNumber)).Where(ord => ord.UniqueID == OrderNumber).ToList();
            ProductsT = (await ProductService.GetAll()).ToList();

            Mapper.Map(OrdersT, Orders);
            Mapper.Map(ProductsT, Products);
        }
    }
}
