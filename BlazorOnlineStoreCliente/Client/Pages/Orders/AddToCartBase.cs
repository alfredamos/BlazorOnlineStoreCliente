using AutoMapper;
using BlazorOnlineStoreCliente.Client.Contracts;
using BlazorOnlineStoreCliente.Client.ViewModels;
using BlazorOnlineStoreCliente.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Client.Pages.Orders
{
    public class AddToCartBase : ComponentBase
    {
        [Inject]
        public ICustomerService CustomerService { get; set; }

        [Inject]
        public IOrderService OrderService { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IShoppingCart ShoppingCart { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Parameter]
        public int ProductId { get; set; }

        public int Quantity { get; set; } = 1;

        public string ProductName { get; set; }

        protected bool ShowOrder = true;

        protected bool ShowCustomerInfo = false;

        public List<OrderLineItem> OrderLineItems { get; set; } = new List<OrderLineItem>();

        public List<Product> Products { get; set; } = new List<Product>();

        public Product Product { get; set; } = new Product();

        public Order OrderT { get; set; } = new Order();

        public OrderView Order { get; set; } = new OrderView();

        public Customer CustomerT { get; set; } = new Customer();

        public CustomerView Customer { get; set; } = new CustomerView();

        public List<Customer> CustomersT { get; set; } = new List<Customer>();

        public List<CustomerView> Customers { get; set; } = new List<CustomerView>();

        protected double TotalPurchase { get; set; }

        public string UserName { get; set; }

        private AuthenticationState authState;

        protected override async Task OnInitializedAsync()
        {
            authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated) UserName = user.Identity.Name;

            CustomersT = (await CustomerService.GetAll()).ToList();
            Mapper.Map(CustomersT, Customers);

            Customer = Customers.FirstOrDefault(cst => cst.Email == UserName);             

            if (Customer == null) InstantiateNullCustomer(); //----> Initialize the customer with a victitious email to prevent the program from breaking when it is null.

            Products = (await ProductService.GetAll()).ToList();
            Product = Products.FirstOrDefault(pd => pd.ProductID == ProductId);

            ShoppingCart.OnChange += StateHasChanged;

            OrderLineItems = ShoppingCart.AddProductToOrder(Product, Quantity);
        }

        protected double TotalPriceOfProducts()
        {
            double totalPurchase = 0.0;

            OrderLineItems = ShoppingCart.GetAllOrderLineItems();

            foreach (var orderLineItem in OrderLineItems)
                totalPurchase += orderLineItem.Price * orderLineItem.Quantity;

            return totalPurchase;
        }

        protected void UpdateQuantity(int productId, int quantity)
        {
            ShoppingCart.UpdateQuantity(productId, quantity);
        }

        protected void RemoveProduct(int productId)
        {
            ShoppingCart.DeleteProductFromOrder(productId);
        }


        protected void PlaceOrder()
        {            
            if (CustomerInformationIsNotNull(Customer)) PlacedOrderHelpMethod(Customer);            
            else
            {                
                ShowOrder = false;
                ShowCustomerInfo = true;                 
            }
        }

        protected async void CreateOrder()
        {
            Mapper.Map(Customer, CustomerT);
            var customert = (await CustomerService.GetAll()).FirstOrDefault(cus => cus.Email == CustomerT.Email);
            if ( customert == null) customert = await CustomerService.Add(CustomerT);
            Mapper.Map(customert, Customer);
            PlacedOrderHelpMethod(Customer);

        }

        protected void Cancel()
        {
            ShowOrder = true;
            ShowCustomerInfo = false;
            NavigationManager.NavigateTo($"/addToCart/{ProductId}");
        }

        protected void ContinueShopping()
        {
            NavigationManager.NavigateTo("/productList");
        }

        protected void Back()
        {
            NavigationManager.NavigateTo("/productList");
        }

        private async void PlacedOrderHelpMethod(CustomerView customer)
        {
            Mapper.Map(customer, CustomerT);
            Mapper.Map(Order, OrderT);

            OrderT.CustomerID = CustomerT.CustomerID;
            OrderT.AdminUser = "admin";

            var result = await ShoppingCart.PlaceOrder(OrderT, CustomerT);

            if (result != null)
            {
                NavigationManager.NavigateTo($"/placeOrder/{result}");
            }
        }

        protected bool CustomerInformationIsNotNull(CustomerView customer)
        {
            if (string.IsNullOrWhiteSpace(customer.CustomerName) || string.IsNullOrWhiteSpace(customer.CustomerAddress) ||
                string.IsNullOrWhiteSpace(customer.CustomerCity) || string.IsNullOrWhiteSpace(customer.CustomerProvince) ||
                string.IsNullOrWhiteSpace(customer.CustomerCountry) || string.IsNullOrWhiteSpace(customer.Email) ||
                string.IsNullOrWhiteSpace(customer.Phone)) return false;

            return true;
        }

        private void InstantiateNullCustomer()
        {
            if (Customer == null) Customer = new CustomerView { Email = "joedoe@gmail.com" }; //----> Initialize the customer with a victitious email to prevent the program from breaking when it is null.
        }

    }
}
