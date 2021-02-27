using BlazorOnlineStoreCliente.Client.ViewModels;
using BlazorOnlineStoreCliente.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Client.Contracts
{
    public interface IAddCardAndAddressToCustomerInfo
    {
        event Action OnChange;
        public bool ClearAddresses { get; set; }
        public bool ClearCardDetails { get; set; }

        List<AddressView> AddAddressToCustomerInfo(AddressView address);
        void ClearList();
        List<CardDetailView> AddCardDetailsToCustomerInfo(CardDetailView card, BillingAddress address);
    }
}
