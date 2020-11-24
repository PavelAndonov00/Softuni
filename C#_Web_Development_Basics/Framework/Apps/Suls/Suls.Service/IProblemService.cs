using Suls.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Service
{
    public interface IProblemService
    {
        IEnumerable<Problem> GetAll();

        Problem CreateProblem(string name, int points);

        Problem GetProblemById(string id);
    }
}
