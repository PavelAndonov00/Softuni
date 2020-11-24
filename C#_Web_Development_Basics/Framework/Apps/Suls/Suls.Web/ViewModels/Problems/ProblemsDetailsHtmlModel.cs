using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Web.ViewModels.Problems
{
    public class ProblemsDetailsHtmlModel
    {
        public string Name { get; set; }

        public string Username { get; set; }

        public int AchievedResult { get; set; }

        public int MaxPoints { get; set; }

        public DateTime CreatedOn { get; set; }

        public string SubmissionId { get; set; }
    }
}
