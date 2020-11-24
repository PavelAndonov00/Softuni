using Musaca.App.ViewModels.Home;
using Musaca.Services;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Result;
using SIS.MvcFramework.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Musaca.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrdersService ordersService;

        public HomeController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        [HttpGet(Url = "/")]
        public IActionResult IndexSlash()
        {
            return Index();
        }

        public IActionResult Index()
        {
            if(this.IsLoggedIn())
            {
                var orderProducts = this.ordersService
                    .GetAllOrderProductsByCashierId(this.User.Id).ToList();
                    //.AsQueryable()
                    //.To<HomeIndexLoggedInOrderProductsModel>();
                //return this.View(orderProducts);
            }

            return this.View();
        }
    }
}
