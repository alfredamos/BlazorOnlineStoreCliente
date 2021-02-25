using AutoMapper;
using BlazorOnlineStoreCliente.Client.Contracts;
using BlazorOnlineStoreCliente.Client.Helpers;
using BlazorOnlineStoreCliente.Client.Mappings;
using BlazorOnlineStoreCliente.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("BlazorOnlineStoreCliente.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorOnlineStoreCliente.ServerAPI"));

            builder.Services.AddApiAuthorization();

            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IProductService, ProductService>();            
            builder.Services.AddScoped<IShoppingCart, ShoppingCart>();
            builder.Services.AddScoped<IOrderCertifyer, OrderCertifyer>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderLineItemService, OrderLineItemService>();
            builder.Services.AddScoped<IPlaceOrder, PlaceOrder>();
            builder.Services.AddScoped<IProcessedOrder, ProcessedOrder>();

            builder.Services.AddAutoMapper(typeof(Maps));

            builder.Services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
                options.AppendTrailingSlash = true;
            });


            await builder.Build().RunAsync();
        }
    }
}
