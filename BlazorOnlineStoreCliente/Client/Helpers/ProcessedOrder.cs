using AutoMapper;
using BlazorOnlineStoreCliente.Client.Contracts;
using BlazorOnlineStoreCliente.Client.ViewModels;
using BlazorOnlineStoreCliente.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Client.Helpers
{
    public class ProcessedOrder : IProcessedOrder
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public event Action OnChange;
        public Order Order { get; set; } = new Order();

        public ProcessedOrder(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task<Order> UpdateOrder(OrderView orderView)
        {
            Console.WriteLine("I'm in UpdateOrder, OrderId : " + orderView.OrderID);
            _mapper.Map(orderView, Order);
            Order.DateProcessed = DateTime.Now;
            var order = await _orderService.Update(Order);
            NotifyDataChanged();

            return order;
        }

        private void NotifyDataChanged() => OnChange.Invoke();
    }
}
