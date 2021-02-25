using AutoMapper;
using BlazorOnlineStoreCliente.Client.Contracts;
using BlazorOnlineStoreCliente.Client.ViewModels;
using BlazorOnlineStoreCliente.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Client.Pages.Products
{
    public class ProductDetailsBase : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IShoppingCart ShoppingCart { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public int Id { get; set; }

        public List<OrderLineItem> OrderLineItems { get; set; } = new List<OrderLineItem>();

        public ProductView Product { get; set; } = new ProductView();

        public Product ProductT { get; set; } = new Product();

        public string ProductName { get; set; }

        public string ProductPrice { get; set; }

        protected bool IsShowProduct = true;

        protected override async Task OnInitializedAsync()
        {
            ProductT = await ProductService.GetById(Id);

            Mapper.Map(ProductT, Product);

            ProductName = Product.Name;
            ProductPrice = Product.Price.ToString("c");

            //ShoppingCart.OnChange += StateHasChanged;
       
        }

        //protected void AddToCart()
        //{                      
        //    OrderLineItems = ShoppingCart.AddProductToOrder(ProductT, Quantity);
        //    IsShowProduct = false;
        //    StateHasChanged();           
        //}

        protected void AddProductToCart(int productId)
        {
            NavigationManager.NavigateTo($"/addToCart/{productId}");
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/productList");
        }

    }
}
