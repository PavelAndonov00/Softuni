using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluffy_Duffy_Munchkin_Cats.Services;
using Fluffy_Duffy_Munchkin_Cats.ViewModels.Cats;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fluffy_Duffy_Munchkin_Cats.Controllers
{
    public class CatsController : Controller
    {
        private readonly ICatServices catServices;

        public CatsController(ICatServices catServices)
        {
            this.catServices = catServices;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CatsAddInputModel cat)
        {
            if (!ModelState.IsValid)
            {
                return this.Redirect("/Cats/Add");
            }

            catServices.Add(cat.Name, cat.Age, cat.Breed, cat.ImageUrl);

            return this.Redirect("/");
        }

        public IActionResult Details(string id)
        {
            var cat = catServices.GetById(id);

            return this.View(cat);
        }
    }
}