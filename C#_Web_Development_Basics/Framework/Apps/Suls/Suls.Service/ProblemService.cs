using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Suls.Data;
using Suls.Models;

namespace Suls.Service
{
    public class ProblemService : IProblemService
    {
        private readonly SulsDbContext context;

        public ProblemService(SulsDbContext context)
        {
            this.context = context;
        }

        public Problem CreateProblem(string name, int points)
        {
            var problem = new Problem
            {
                Name = name,
                Points = points
            };

            this.context.Problems.Add(problem);
            this.context.SaveChanges();

            return problem;
        }

        public IEnumerable<Problem> GetAll()
        {
            var problems = this
                .context
                .Problems
                .Include(p => p.Submissions)
                .ThenInclude(s => s.User)
                .Include(p => p.Submissions)
                .ThenInclude(s => s.Problem)
                .ToList();

            return problems;
        }

        public Problem GetProblemById(string id)
        {
            var problem = this
                .context
                .Problems
                .Include(p => p.Submissions)
                .ThenInclude(s => s.User)
                .Include(p => p.Submissions)
                .ThenInclude(s => s.Problem)
                .FirstOrDefault(p => p.Id == id);

            return problem;
        }
    }
}
