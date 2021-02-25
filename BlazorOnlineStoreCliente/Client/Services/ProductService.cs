using BlazorOnlineStoreCliente.Client.Contracts;
using BlazorOnlineStoreCliente.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Client.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "api/products";

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Product> Add(Product newService)
        {
            return await _httpClient.PostJsonAsync<Product>($"{_baseUrl}", newService);
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _httpClient.GetJsonAsync<Product[]>(_baseUrl);
        }

        public async Task<Product> GetById(int id)
        {
            return await _httpClient.GetJsonAsync<Product>($"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<Product>> Search(string searchKey)
        {
            return await _httpClient.GetJsonAsync<Product[]>($"{_baseUrl}/{searchKey}");
        }

        public async Task<Product> Update(Product updatedService)
        {
            Console.WriteLine("In Product Service, Product Name : " + updatedService.Name);
            Console.WriteLine("In Product Service, Product Brand : " + updatedService.Brand);

            return await _httpClient.PutJsonAsync<Product>($"{_baseUrl}/{updatedService.ProductID}", updatedService);
        }
    }
}
