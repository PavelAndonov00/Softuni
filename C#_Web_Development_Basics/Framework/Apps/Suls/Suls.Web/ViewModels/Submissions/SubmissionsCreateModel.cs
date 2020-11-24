using SIS.MvcFramework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Web.ViewModels.Submissions
{
    public class SubmissionsCreateModel
    {
        public string ProblemId { get; set; }


        [StringLengthSis(30, 800, "Invalid Code!")]
        public string Code { get; set; }
    }
}
