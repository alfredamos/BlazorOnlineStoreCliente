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
    public class OrderProcessedBase : ComponentBase
    {
        [Inject]
        public IOrderService OrderService { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        public List<OrderView> Orders { get; set; } = new List<OrderView>();

        public List<Order> OrdersT { get; set; } = new List<Order>();

        public List<ProductView> Products { get; set; } = new List<ProductView>();

        public List<Product> ProductsT { get; set; } = new List<Product>();

        public double Total { get; set; } = 0.0;

        public double SubTotal { get; set; } = 0.0;

        protected override async Task OnInitializedAsync()
        {            
            ProductsT = (await ProductService.GetAll()).ToList();
            OrdersT = (await OrderService.GetAll()).Where(od => od.DateProcessed.HasValue)
                      .OrderByDescending(od => od.DateProcessed).ToList();

            Mapper.Map(OrdersT, Orders);
            Mapper.Map(ProductsT, Products);

        }

        protected async void HandleSearch(string searchKey)
        {
            OrdersT = (await OrderService.Search(searchKey)).Where(od => od.DateProcessed.HasValue)
                      .OrderByDescending(od => od.DateProcessed).ToList();

            Mapper.Map(OrdersT, Orders);
        }

    }
}

