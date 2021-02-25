using BlazorOnlineStoreCliente.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Client.Contracts
{
    public interface IPlaceOrder
    {
        Task<Order> CreateOrder(Order order, Customer customer);
        Task<Order> UpdateOrder(Order order, Customer customer);
    }
}

