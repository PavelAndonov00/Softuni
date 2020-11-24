using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using Suls.Service;
using Suls.Web.ViewModels.Problems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suls.Web.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemService problemService;

        public ProblemsController(IProblemService problemService)
        {
            this.problemService = problemService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(ProblemsCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Problems/Create");
            }

            this.problemService.CreateProblem(model.Name, model.Points);

            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult Details(ProblemsDetailsModel input)
        {
            var problem = this.problemService.GetProblemById(input.Id);
            var submissions = problem.Submissions
                .Select(s => new ProblemsDetailsHtmlModel
                {
                    Username = s.User.Username,
                    AchievedResult = s.AchievedResult,
                    MaxPoints = s.Problem.Points,
                    CreatedOn = s.CreatedOn,
                    SubmissionId = s.Id
                })
                .ToList();

            return this.View(new ProblemsDetailsWrapper { Name = problem.Name, Problems = submissions});
        }

    }
}
