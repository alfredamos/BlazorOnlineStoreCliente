using AutoMapper;
using BlazorOnlineStoreCliente.Client.Contracts;
using BlazorOnlineStoreCliente.Client.ViewModels;
using BlazorOnlineStoreCliente.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Client.Pages.Customers
{
    public class CustomerDetailsBase : ComponentBase
    {
        [Inject]
        public ICustomerService CustomerService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public int Id { get; set; }

        public Customer CustomerT { get; set; } = new Customer();

        public CustomerView Customer { get; set; } = new CustomerView();

        protected bool ShowFooter = false;

        protected async override Task OnInitializedAsync()
        {
            CustomerT = await CustomerService.GetById(Id);

            Mapper.Map(CustomerT, Customer);
        }

        protected void UpdateCustomer()
        {           
            
            NavigationManager.NavigateTo($"/editCustomer/{Id}");
           
        }

        protected void DeleteCustomer()
        {

            NavigationManager.NavigateTo($"/deleteCustomer/{Id}");

        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/customerList");
        }

        protected void ShowFooterMethod()
        {
            ShowFooter = true;
        }

        protected void HideFooterMethod()
        {
            ShowFooter = false;
        }
    }
}

