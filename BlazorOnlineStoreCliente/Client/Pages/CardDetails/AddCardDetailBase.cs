using AutoMapper;
using Blazored.Toast.Services;
using BlazorOnlineStoreCliente.Client.Contracts;
using BlazorOnlineStoreCliente.Client.ViewModels;
using BlazorOnlineStoreCliente.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Client.Pages.CardDetails
{
    public class AddCardDetailBase : ComponentBase
    {
        [Inject]
        public ICardDetailService  CardDetailService{ get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        public BillingAddress CardAddress { get; set; } = new BillingAddress();

        public CardDetail CardDetailT { get; set; } = new CardDetail();

        public CardDetailView CardDetail { get; set; } = new CardDetailView();

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        protected async Task CreateCardDetail()
        {
            Mapper.Map(CardDetail, CardDetailT);
            
            CardDetailT.BillingAddress = CardAddress;            
            
            var result = await CardDetailService.Add(CardDetailT);           

            if (result != null)
            {
                ToastService.ShowSuccess("Card Details is successfully created");

                NavigationManager.NavigateTo("/cardDetailList");
            }

        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/cardDetailList");
        }

    }
}
