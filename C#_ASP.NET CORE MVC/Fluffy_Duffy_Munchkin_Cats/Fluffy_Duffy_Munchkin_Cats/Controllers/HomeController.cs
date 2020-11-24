using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fluffy_Duffy_Munchkin_Cats.Models;
using Fluffy_Duffy_Munchkin_Cats.Services;

namespace Fluffy_Duffy_Munchkin_Cats.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatServices catServices;

        public HomeController(ICatServices catServices)
        {
            this.catServices = catServices;
        }

        public IActionResult Index()
        {
            var cats = catServices.GetAll();
            return View(cats);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
