﻿using AutoMapper;
using Blazored.Toast.Services;
using BlazorOnlineStoreCliente.Client.Contracts;
using BlazorOnlineStoreCliente.Client.Helpers;
using BlazorOnlineStoreCliente.Client.ViewModels;
using BlazorOnlineStoreCliente.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Client.Pages.Orders
{
    public class OrderUpdatedBase : ComponentBase
    {
        [Inject]
        public IOrderService OrderService { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IShoppingCart ShoppingCart { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public string UniqueID { get; set; }

        public OrderView Order { get; set; } = new OrderView();

        public Order OrderT { get; set; } = new Order();

        public List<Product> ProductsT { get; set; } = new List<Product>();

        public List<ProductView> Products { get; set; } = new List<ProductView>();
        public List<OrderLineItem> OrderLineItems { get; private set; } = new List<OrderLineItem>();

        protected async override Task OnInitializedAsync()
        {
            OrderT = (await OrderService.GetAll()).FirstOrDefault(odt => odt.UniqueID == UniqueID);
            Mapper.Map(OrderT, Order);

            ShoppingCart.OnChange += StateHasChanged;

            OrderLineItems = ShoppingCart.GetAllOrderLineItems(OrderT.OrderLineItems);
           
            ProductsT = (await ProductService.GetAll()).ToList();
            Mapper.Map(ProductsT, Products);
        }

        protected async Task UpdatedOrder()
        {            
            Order.OrderLineItems = OrderLineItems;
       
            OrderT.OrderLineItems = Order.OrderLineItems;
           
            var result = await ShoppingCart.UpdateOrder(OrderT, OrderT.Customer);
           
            if (result != null && result.DateProcessed.HasValue)
            {
                NavigationManager.NavigateTo("/orderProcessed");
            }
            else if (result != null && !result.DateProcessed.HasValue)
            {
                ToastService.ShowSuccess("Customer order is successfully updated.");

                NavigationManager.NavigateTo("/orderList");
            }
        }

        protected void UpdateQuantity(OrderLineItem orderLineItem)
        {
            var productId = orderLineItem.ProductID;
            var quantity = orderLineItem.Quantity;
            ShoppingCart.UpdateQuantity(productId, quantity);
        }

        protected void Cancel()
        {
            if (Order.DateProcessed.HasValue)
            {
                NavigationManager.NavigateTo("/orderProcessed");
            }
            else if (!Order.DateProcessed.HasValue)
            {
                NavigationManager.NavigateTo("/orderList");
            }
        }

    }
}
