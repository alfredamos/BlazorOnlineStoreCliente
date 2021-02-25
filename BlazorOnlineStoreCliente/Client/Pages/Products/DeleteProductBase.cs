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
    public class DeleteProductBase : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public int Id { get; set; }

        public Product ProductT { get; set; } = new Product();

        public ProductView Product { get; set; } = new ProductView();

        protected override async Task OnInitializedAsync()
        {
            ProductT = await ProductService.GetById(Id);

            Mapper.Map(ProductT, Product);
        }

        protected async Task DeleteProduct()
        {
            Mapper.Map(Product, ProductT);

            await ProductService.Delete(Id);

            NavigationManager.NavigateTo("/productList");
           
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/productList");
        }
    }
}
