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
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "api/orders";

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Order> Add(Order newService)
        {
            return await _httpClient.PostJsonAsync<Order>($"{_baseUrl}", newService);
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _httpClient.GetJsonAsync<Order[]>(_baseUrl);
        }

        public async Task<Order> GetById(int id)
        {
            return await _httpClient.GetJsonAsync<Order>($"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<Order>> Search(string searchKey)
        {
            return await _httpClient.GetJsonAsync<Order[]>($"{_baseUrl}/{searchKey}");
        }

        public async Task<Order> Update(Order updatedService)
        {
            return await _httpClient.PutJsonAsync<Order>($"{_baseUrl}/{updatedService.OrderID}", updatedService);
        }
    }
}
