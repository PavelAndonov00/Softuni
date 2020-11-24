using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using Suls.Models;
using Suls.Service;
using Suls.Web.ViewModels.Submissions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Web.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ISubmissionService submissionService;
        private readonly IProblemService problemService;
        private readonly IUsersService usersService;

        public SubmissionsController(ISubmissionService submissionService, IProblemService problemService, IUsersService usersService)
        {
            this.submissionService = submissionService;
            this.problemService = problemService;
            this.usersService = usersService;
        }

        [Authorize]
        public IActionResult Create(SubmissionsCreateInputModel model)
        {
            var currentProblem = this
                .problemService
                .GetProblemById(model.Id);
            var createModel = new SubmissionsCreateHtmlModel
            {
                Name = currentProblem.Name,
                ProblemId = currentProblem.Id
            };

            return this.View(createModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(SubmissionsCreateModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var problem = this
                .problemService
                .GetProblemById(model.ProblemId);
                var createModel = new SubmissionsCreateHtmlModel
                {
                    Name = problem.Name,
                    ProblemId = problem.Id
                };

                return this.View(createModel);
            }

            var currentProblem = this.problemService.GetProblemById(model.ProblemId);
            var currentUser = this.usersService.GetUserById(this.User.Id);
            this.submissionService.CreateSubmission(model.Code, currentProblem.Points, currentProblem.Id, currentUser.Id);
            
            return this.Redirect("/");
        }

        public IActionResult Delete(SubmissionsDeleteModel model)
        {
            this.submissionService.DeleteSubmission(model.Id);

            return this.Redirect("/");
        }
    }
}
