﻿using AutoMapper;
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
    public class CardDetailListBase : ComponentBase
    {
        [Inject]
        public ICardDetailService CardDetailService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        public List<CardDetail> CardDetailsT { get; set; } = new List<CardDetail>();

        public List<CardDetailView> CardDetails { get; set; } = new List<CardDetailView>();

        protected async override Task OnInitializedAsync()
        {
            CardDetailsT = (await CardDetailService.GetAll()).ToList();

            Mapper.Map(CardDetailsT, CardDetails);
        }

        protected void UpdateCardDetails(int cardId)
        {
            NavigationManager.NavigateTo($"/editCardDetail/{cardId}");
        }

        protected void DeleteCardDetails(int cardId)
        {
            NavigationManager.NavigateTo($"/deleteCardDetail/{cardId}");
        }

        protected void CreateCardDetails()
        {
            NavigationManager.NavigateTo("/addCardDetail");
        }

        protected async Task HandleSearch(string searchKey)
        {
            CardDetailsT = (await CardDetailService.Search(searchKey)).ToList();

            Mapper.Map(CardDetailsT, CardDetails);
        }
    }
}
