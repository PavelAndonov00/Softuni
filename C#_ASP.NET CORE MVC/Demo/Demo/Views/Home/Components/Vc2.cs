using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.TagHelpers
{
    [ViewComponent (Name = "MyComponent")]
    public class Vc2 : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string name)
        {
            return this.View(viewName: "MyCompView", new { name });
        }
    }
}
