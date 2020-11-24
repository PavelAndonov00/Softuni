using Suls.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Service
{
    public interface ISubmissionService
    {
        Submission CreateSubmission(string code, int problemPoints, string problemId, string userId);

        void DeleteSubmission(string id);
    }
}
