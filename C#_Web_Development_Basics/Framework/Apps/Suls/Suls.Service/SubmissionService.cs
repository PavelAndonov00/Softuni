using SIS.MvcFramework.Attributes.Security;
using Suls.Data;
using Suls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suls.Service
{
    public class SubmissionService : ISubmissionService
    {
        private readonly SulsDbContext context;
        private readonly Random rnd;

        public SubmissionService(SulsDbContext context)
        {
            this.context = context;
            this.rnd = new Random();
        }

        public Submission CreateSubmission(string code, int problemPoints, string problemId, string userId)
        {
            var submission = new Submission
            {
                Code = code,
                AchievedResult = rnd.Next(0, problemPoints),
                CreatedOn = DateTime.UtcNow,
                UserId = userId,
                ProblemId = problemId
            };

            this.context.Submissions.Add(submission);
            this.context.SaveChanges();

            return submission;
        }

        public void DeleteSubmission(string id)
        {
            var submission = this.context.Submissions.FirstOrDefault(s => s.Id == id);
            this.context.Submissions.Remove(submission);
            this.context.SaveChanges();
        }
    }
}
