using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Result;
using SIS.MvcFramework.Mapping;
using Suls.Service;
using Suls.Web.ViewModels.Home;
using System.Linq;

namespace SULS.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProblemService problemService;

        public HomeController(IProblemService problemService)
        {
            this.problemService = problemService;
        }

        [HttpGet(Url = "/")]
        public IActionResult IndexSlash()
        {
            return Index();
        }

        public IActionResult Index()
        {
            if (this.IsLoggedIn())
            {
                var problems = this
                    .problemService
                    .GetAll()
                    .Select(p => new HomeLoggedInModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Count = p.Submissions.Count
                    })
                    .ToList();

                return this.View(problems, "IndexLoggedIn");
            }
            else
            {
                return this.View();
            }
        }
    }
}