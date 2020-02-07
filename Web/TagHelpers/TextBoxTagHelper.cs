using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Web.TagHelpers
{
    public class TextBoxTagHelper : TagHelper
    {
        private HtmlHelper _htmlHelper;
        private HtmlEncoder _htmlEncoder;

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        public TextBoxTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder)
        {
            _htmlHelper = htmlHelper as HtmlHelper;
            _htmlEncoder = htmlEncoder;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var view = await _htmlHelper.PartialAsync("_string", For);
            var writer = new StringWriter();
            view.WriteTo(writer, _htmlEncoder);
            output.Content.SetHtmlContent(writer.ToString());
        }
    }
}