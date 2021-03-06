﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Emusic.Data;
using Emusic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Emusic.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    /// 
    public class ShopController : Controller

    {
        private readonly ProductDbContext _productDbContext;

        /// <summary>
        /// 
        /// </summary>
      

        public ShopController(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public IActionResult Index()
        {
            return View(_productDbContext.Products);
        }




        [Authorize(Policy = "CountryMusicOnly")]
        public async Task<IActionResult> CountryMusicOnly()
        {
            //return View(new ProductViewModel()
            {
                return View("Country Lovers");
                //Products = await _productDbContext.Products.ToListAsync()
            }

        }


        /// <summary>
        /// 
        /// </summary>
     /*
        public async Task<IAsyncResult> Details(long? id)
        {
            Product product;

            if (!id.HasValue || (product = await _productDbContext.Products.FindAsync(id.Value)) is null)
            {
                return NotFound();
            }


            return View(product);

        }
       
    */

    }
}
