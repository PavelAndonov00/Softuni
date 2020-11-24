using SIS.MvcFramework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Web.ViewModels.Problems
{
    public class ProblemsCreateInputModel
    {
        [StringLengthSis(5, 20, "Invalid name!")]
        public string Name { get; set; }

        [RangeSis(50, 300, "Invalid Points!")]
        public int Points { get; set; }
    }
}
