using Panda.App.ViewModels.Packages;
using Panda.Services;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Panda.App.Controllers
{
    public class PackagesController : Controller
    {
        private readonly IPackageService packageService;
        private readonly IUsersService usersService;
        private readonly IReceiptsService receiptsService;

        public PackagesController(IPackageService packageService, IUsersService usersService, IReceiptsService receiptsService)
        {
            this.packageService = packageService;
            this.usersService = usersService;
            this.receiptsService = receiptsService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var users = usersService.GetAllUsersNamesWithoutCurrentLogged(this.User.Id).ToList();
            return this.View(users);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(PackageCreateModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Packages/Create");
            }

            this.packageService.CreatePackage(model.Description, model.Weight, model.ShippingAddress, model.RecipientName);

            return this.Redirect("/Packages/Pending");
        }

        [Authorize]
        public IActionResult Deliver(PackageDeliverModel model)
        {
            var package = this.packageService.ChangePackageToDeliveredStatusById(model.id);

            receiptsService.CreateReceipt(package.Weight * 2.67M, package.RecipientId, package.Id);

            return this.Redirect("/Packages/Delivered");
        }

        [Authorize]
        public IActionResult Delivered()
        {
            var packages = this.packageService
                .GetAllPackageByCurrentLoggedUserIdAndStatus(this.User.Id, "Delivered")
                .Select(p => new PackagesPendingModel
                {
                    Description = p.Description,
                    ShippingAddress = p.ShippingAddress,
                    Weight = p.Weight,
                    RecipientName = p.Recipient.Username,
                    Id = p.Id
                }).ToList();

            return this.View(new Wrapper { Packages = packages });
        }

        [Authorize]
        public IActionResult Pending()
        {
            var packages = this.packageService
                .GetAllPackageByCurrentLoggedUserIdAndStatus(this.User.Id, "Pending")
                .Select(p => new PackagesPendingModel
                {
                    Description = p.Description,
                    ShippingAddress = p.ShippingAddress,
                    Weight = p.Weight,
                    RecipientName = p.Recipient.Username,
                    Id = p.Id
                }).ToList();
            return this.View(packages);
        }
    }
}
