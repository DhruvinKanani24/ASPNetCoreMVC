using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webgentle.BookStore.Helper
{
    public class CustomEmailTagHelper : TagHelper
    {
        //public string MyEmail { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href","mailto:kananidhruvin@gmail.com");
            //output.Attributes.SetAttribute("href", $"mailto:{MyEmail}");
            output.Attributes.Add("Id","my-email-id");
            output.Content.SetContent("my-Email");
        }
    }
}
