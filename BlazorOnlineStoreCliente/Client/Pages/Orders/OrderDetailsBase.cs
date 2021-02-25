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
    public class OrderDetailsBase : ComponentBase
    {
        [Inject]
        public IOrderService OrderService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public int Id { get; set; }

        public OrderView Order { get; set; } = new OrderView();

        public Order OrderT { get; set; } = new Order();

        protected override async Task OnInitializedAsync()
        {
            OrderT = await OrderService.GetById(Id);

            Mapper.Map(OrderT, Order);
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/orderList");
        }

        protected void DeleteOrder()
        {
            NavigationManager.NavigateTo($"/deleteOrder/{Id}");
        }

        protected void UpdateOrder()
        {
            NavigationManager.NavigateTo($"/editOrder/{Id}");
        }

    }
}
