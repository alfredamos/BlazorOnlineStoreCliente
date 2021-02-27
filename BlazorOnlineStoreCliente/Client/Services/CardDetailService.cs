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
    public class CardDetailService : ICardDetailService
    {
        private readonly HttpClient _httpClient;
        private string _baseUrl = "api/cardDetails";

        public CardDetailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<CardDetail> Add(CardDetail newService)
        {           
            return await _httpClient.PostJsonAsync<CardDetail>($"{_baseUrl}", newService);
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<CardDetail>> GetAll()
        {
            return await _httpClient.GetJsonAsync<CardDetail[]>($"{_baseUrl}");
        }

        public async Task<CardDetail> GetById(int id)
        {
            return await _httpClient.GetJsonAsync<CardDetail>($"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<CardDetail>> Search(string searchKey)
        {
            return await _httpClient.GetJsonAsync<CardDetail[]>($"{_baseUrl}/{searchKey}");
        }

        public async Task<CardDetail> Update(CardDetail updatedService)
        {
            return await _httpClient.PutJsonAsync<CardDetail>($"{_baseUrl}/{updatedService.CardDetailID}", updatedService);
        }

    }
}
