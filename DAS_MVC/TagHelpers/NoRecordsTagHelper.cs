using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DAS_MVC.TagHelpers
{
    public class NoRecordsTagHelper : TagHelper
    {
        [HtmlAttributeName("Message")]
        public string Message { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(
                string.Format(@"
                                <div class=""text-center"">
                                    <h2><span class=""label label-warning"">{0}</span></h2>
                                </div>
                            ",
                        Message)
            );

            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}
