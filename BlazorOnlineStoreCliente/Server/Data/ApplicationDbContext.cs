using BlazorOnlineStoreCliente.Server.Models;
using BlazorOnlineStoreCliente.Shared.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(new Product
            {
                ProductID = 1,
                Brand = "Dolce Gabana",
                Name = "Denim Shoe",
                Price = 45.0,
                ImageLink = "https://mdbootstrap.com/img/Photos/Horizontal/E-commerce/Orders/shoes%20(2).jpg",
                Description = "Shiny Bright Shoe"
            },
            new Product
            {
                ProductID = 2,
                Brand = "Givenchy",
                Name = "Rolex Belt",
                Price = 65.0,
                ImageLink = "https://mdbootstrap.com/img/Photos/Horizontal/E-commerce/Orders/belt.jpg",
                Description = "Rolex Belt"
            },
            new Product
            {
                 ProductID = 3,
                 Brand = "Plasmot Special",
                 Name = "Shirt and Shoe Combo",
                 Price = 70.0,
                 ImageLink = "https://mdbootstrap.com/img/Photos/Horizontal/E-commerce/Orders/img%20(4).jpg",
                 Description = "Shirt & Shoe"
             },
            new Product
            {
                 ProductID = 4,
                 Brand = "Romano Sport",
                 Name = "Greeno Sport Shoe",
                 Price = 65.0,
                 ImageLink = "https://mdbootstrap.com/img/Photos/Horizontal/E-commerce/Orders/shoes%20(3).jp",
                 Description = "Sport Shoe"
            }
            );

            builder.Entity<OrderLineItem>()
            .HasOne(d => d.Order)
            .WithMany(ds => ds.OrderLineItems)
            .HasForeignKey(d => d.OrderID);            

            base.OnModelCreating(builder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLineItem> OrderLineItems { get; set; }
        public DbSet<Product> Products { get; set; }
        

    }
}
