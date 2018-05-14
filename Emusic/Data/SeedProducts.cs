using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Emusic.Models;

namespace Emusic.Data
{
    public static class SeedProducts
    {
        private static List<Product> Products{ get; } = new List<Product>
        {
            new Product()
            {
                Name = " ReD hot chili Peppers Tickets",
                Description= "the hottest upcoming events and concerts in LA ",
                ImageHref="/images/Chili.jpg",
                Price = 1.00M,
            },

            new Product()
            {
                Name = "Steve Young Tickets ",
                Description= "the hottest upcoming events and concerts in LA ",
                ImageHref="/images/GarthBrooks.png",
                Price = 1.00M,
            },

           new Product()
            {
                Name = "Bruno Mars Tickets ",
                Description= "the hottest upcoming events and concerts in LA ",
                ImageHref="/images/Bruno.jpg",
                Price = 1.00M,
            },

           new Product()
            {
                Name = "VIP Premium ",
                Description= "the Best seats and the hottest upcoming events and concerts Today ",
                ImageHref="/images/Admit-One-Ticket.jpg",
                Price = 2.00M,
            },


        };

        public static async Task Initialize(IServiceProvider services)
        {

            using (ProductDbContext context = new ProductDbContext(
                services.GetRequiredService<DbContextOptions<ProductDbContext>>()))
            {
                if (await context.Products.AnyAsync())
                {
                    return;
                }

                await context.Database.EnsureCreatedAsync();
                await context.AddRangeAsync(Products);
                await context.SaveChangesAsync();

            }
        }


    }
}
