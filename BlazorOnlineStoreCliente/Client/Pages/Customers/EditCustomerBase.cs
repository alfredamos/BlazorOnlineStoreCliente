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
    public class EditCustomerBase : ComponentBase
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

        protected async override Task OnInitializedAsync()
        {
            CustomerT = await CustomerService.GetById(Id);

            Mapper.Map(CustomerT, Customer);
        }

        protected async Task UpdateCustomer()
        {
            Mapper.Map(Customer, CustomerT);

            var product = await CustomerService.Update(CustomerT);

            if (product != null)
            {
                NavigationManager.NavigateTo("/customerList");
            }
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/customerList");
        }
    }
}
