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
    public class OrderLineItemService : IOrderLineItemService
    {
        private readonly HttpClient _htppClient;
        private readonly string _baseUrl = "api/orderLineItems";

        public OrderLineItemService(HttpClient htppClient)
        {
            _htppClient = htppClient;
        }
        public async Task<OrderLineItem> Add(OrderLineItem newService)
        {
            return await _htppClient.PostJsonAsync<OrderLineItem>(_baseUrl, newService);
        }

        public async Task Delete(int id)
        {
            await _htppClient.DeleteAsync($"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<OrderLineItem>> GetAll()
        {
            return await _htppClient.GetJsonAsync<OrderLineItem[]>(_baseUrl);
        }

        public async Task<OrderLineItem> GetById(int id)
        {
            return await _htppClient.GetJsonAsync<OrderLineItem>($"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<OrderLineItem>> Search(string searchKey)
        {
            return await _htppClient.GetJsonAsync<OrderLineItem[]>($"{_baseUrl}/{searchKey}");
        }

        public async Task<OrderLineItem> Update(OrderLineItem updatedService)
        {
            return await _htppClient.PutJsonAsync<OrderLineItem>($"{_baseUrl}/{updatedService.OrderLineItemID}", updatedService);
        }
    }
}
