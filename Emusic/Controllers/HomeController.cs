using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Emusic.Models;
using Microsoft.Extensions.Configuration;

namespace Emusic.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; set; }

        public HomeController(IConfiguration config)
        {
            Configuration = config;

            //Rest of class definition
        }





        public IActionResult Index()
        {
            
            //This is adding My user Secrets 
            ViewData["myAppSetting"] = Configuration["myAppSetting"];
            ViewData["myConnectionString"] = Configuration.GetConnectionString("myConnectionString");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "WEB DEVS INC.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = " Email us at Email@Emai.com";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
