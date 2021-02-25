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
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;
        private string _baseUrl = "api/customers";

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Customer> Add(Customer newService)
        {
            return await _httpClient.PostJsonAsync<Customer>($"{_baseUrl}", newService);
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _httpClient.GetJsonAsync<Customer[]>($"{_baseUrl}");
        }

        public async Task<Customer> GetById(int id)
        {
            return await _httpClient.GetJsonAsync<Customer>($"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<Customer>> Search(string searchKey)
        {
            return await _httpClient.GetJsonAsync<Customer[]>($"{_baseUrl}/{searchKey}");
        }

        public async Task<Customer> Update(Customer updatedService)
        {
            return await _httpClient.PutJsonAsync<Customer>($"{_baseUrl}/{updatedService.CustomerID}", updatedService);
        }
    }
}
