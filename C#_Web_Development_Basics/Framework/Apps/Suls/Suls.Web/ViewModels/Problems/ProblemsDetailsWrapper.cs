using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Web.ViewModels.Problems
{
    public class ProblemsDetailsWrapper
    {
        public string Name { get; set; }

        public virtual ICollection<ProblemsDetailsHtmlModel> Problems { get; set; }
    }
}
