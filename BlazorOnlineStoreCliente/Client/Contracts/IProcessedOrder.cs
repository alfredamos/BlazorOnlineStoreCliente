using BlazorOnlineStoreCliente.Client.ViewModels;
using BlazorOnlineStoreCliente.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Client.Contracts
{
    public interface IProcessedOrder
    {
        event Action OnChange;
        Task<Order> UpdateOrder(OrderView orderView);
    }
}
