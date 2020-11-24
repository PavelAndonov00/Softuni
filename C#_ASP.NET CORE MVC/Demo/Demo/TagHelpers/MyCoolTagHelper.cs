using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.TagHelpers
{
    [HtmlTargetElement("h1", Attributes = "cool-attr")]
    public class MyCoolTagHelper : TagHelper
    {
        public string CoolAttr { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.PreContent.SetContent(CoolAttr);
            output.PostContent.SetContent(CoolAttr);

        }
    }
}
