using BlazorOnlineStoreCliente.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Server.Contracts
{
    public interface IOrderLineItemRepository : IBaseRepository<OrderLineItem>
    {
        Task AddRange(List<OrderLineItem> lineItems);
    }
}
