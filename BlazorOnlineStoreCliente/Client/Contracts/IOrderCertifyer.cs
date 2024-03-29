﻿using BlazorOnlineStoreCliente.Shared.Models;

namespace BlazorOnlineStoreCliente.Client.Contracts
{
    public interface IOrderCertifyer
    {
        bool ValidateCreateOrder(Order order, Customer customer);
        bool ValidateCustomerInformation(string name, string address, string city, string state, string country);
        bool ValidateUpdateOrder(Order order, Customer customer);
        bool ValidateProcessOrder(Order order);
    }
}