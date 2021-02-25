using BlazorOnlineStoreCliente.Client.Contracts;
using BlazorOnlineStoreCliente.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Client.Helpers
{
    public class OrderCertifyer : IOrderCertifyer
    {
        public bool ValidateCustomerInformation(
            string name,
            string address,
            string city,
            string province,
            string country)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(province) ||
                string.IsNullOrWhiteSpace(country)) return false;

            return true;
        }

        public bool ValidateCreateOrder(Order order, Customer customer)
        {
            //----> Order has to exist.
            if (order == null) return false;

            //----> Order has to have line items.
            if (order.OrderLineItems == null || order.OrderLineItems.Count <= 0) return false;

            //----> Validating line items.
            foreach (var item in order.OrderLineItems)
            {
                if (item.ProductID <= 0 || item.Price <= 0 || item.Quantity <= 0) return false;
            }

            //----> Validate customer info.
            if (!ValidateCustomerInformation(customer.CustomerName,
                customer.CustomerAddress, customer.CustomerCity,
                customer.CustomerProvince, customer.CustomerCountry)) return false;

            return true;
        }

        public bool ValidateUpdateOrder(Order order, Customer customer)
        {
            //----> Order has to exist.
            if (order == null) return false;
            if (!order.OrderID.Equals(0)) return false;

            //----> Order has to have order line items.
            if (order.OrderLineItems == null || order.OrderLineItems.Count <= 0) return false;
      
            //----> Other dates.
            if (order.DateProcessed.HasValue) return false;

            //----> Validate UniqueID.
            if (string.IsNullOrWhiteSpace(order.UniqueID)) return false;

            //----> Validating line items.
            foreach (var item in order.OrderLineItems)
            {
                if (item.ProductID <= 0 || item.Price <= 0 || item.Quantity <= 0 || 
                    item.OrderID == order.OrderID) return false;
            }

            //----> Validate customer info.
            if (!ValidateCustomerInformation(customer.CustomerName, customer.CustomerAddress,
                customer.CustomerCity, customer.CustomerProvince, customer.CustomerCountry)) return false;

            return true;
        }

        public bool ValidateProcessOrder(Order order)
        {
            if (!order.DateProcessed.HasValue ||
                string.IsNullOrWhiteSpace(order.AdminUser)) return false;

            return true;
        }
    }
}
