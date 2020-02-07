using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Web.TagHelpers
{
    /// <summary>
    /// This will help ensure that scripts are only injected once
    /// </summary>
    [HtmlTargetElement("script", Attributes = "src")]
    public class ScriptTagHelper : TagHelper
    {
        private readonly IScriptManager _scriptManager;

        public ScriptTagHelper(IScriptManager scriptManager, HtmlEncoder htmlEncoder)
        {
            _scriptManager = scriptManager;
            HtmlEncoder = htmlEncoder;
        }

        public string IncludeOrderPriority { get; set; }

        private HtmlEncoder HtmlEncoder { get; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            // needed because of builtin tag helper that looks at src
            var src = GetEncodedStringValue(output.Attributes["src"].Value);
            _scriptManager.AddScript(new ScriptReference(src, Convert.ToInt32(IncludeOrderPriority)));

            await output.GetChildContentAsync();
            output.SuppressOutput();
        }

        private string GetEncodedStringValue(object attributeValue)
        {
            var stringValue = attributeValue as string;
            if (stringValue != null)
            {
                var encodedStringValue = HtmlEncoder.Encode(stringValue);
                return encodedStringValue;
            }

            if (attributeValue is IHtmlContent htmlContent)
            {
                if (htmlContent is HtmlString htmlString)
                {
                    // No need for a StringWriter in this case.
                    stringValue = htmlString.ToString();
                }
                else
                {
                    using var writer = new StringWriter();
                    htmlContent.WriteTo(writer, HtmlEncoder);
                    stringValue = writer.ToString();
                }

                return stringValue;
            }

            return attributeValue.ToString();
        }
    }
}
