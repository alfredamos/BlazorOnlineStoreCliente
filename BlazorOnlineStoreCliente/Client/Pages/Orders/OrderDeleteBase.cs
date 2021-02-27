using AutoMapper;
using Blazored.Toast.Services;
using BlazorOnlineStoreCliente.Client.Contracts;
using BlazorOnlineStoreCliente.Client.Shared;
using BlazorOnlineStoreCliente.Client.ViewModels;
using BlazorOnlineStoreCliente.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Client.Pages.Orders
{
    public class OrderDeleteBase : ComponentBase
    {
        [Inject]
        public IOrderService OrderService { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public string OrderNumber { get; set; }

        public List<CardDetail> CustomersT { get; set; } = new List<CardDetail>();

        public List<CustomerView> Customers { get; set; } = new List<CustomerView>();

        public List<OrderView> Orders { get; set; } = new List<OrderView>();

        public List<Order> OrdersT { get; set; } = new List<Order>();

        public List<ProductView> Products { get; set; } = new List<ProductView>();

        public List<Product> ProductsT { get; set; } = new List<Product>();

        protected ConfirmDelete DeleteConfirmation { get; set; }

        public string CustomerName { get; set; }

        public double SubTotal { get; set; } = 0.0;

        public double Total { get; set; } = 0.0;

        protected override async Task OnInitializedAsync()
        {
            OrdersT = (await OrderService.Search(OrderNumber)).Where(ord => ord.UniqueID == OrderNumber).ToList();
            ProductsT = (await ProductService.GetAll()).ToList();

            CustomerName = OrdersT[0].Customer.FirstName;

            Mapper.Map(OrdersT, Orders);
            Mapper.Map(ProductsT, Products);
        }

        protected void DeleteClick()
        {
            DeleteConfirmation.Show();
        }

        protected async Task DeleteOrder(bool deleteConfirmed)
        {
            Mapper.Map(Orders, OrdersT);

            if (deleteConfirmed) await OrderService.Delete(OrdersT[0].OrderID);

            ToastService.ShowSuccess("Customer order is successfully deleted");

            NavigationManager.NavigateTo("/orderList");

        }     

    }
}
