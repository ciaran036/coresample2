using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Web.TagHelpers
{
    [HtmlTargetElement("submit-button")]
    public class SubmitButtonTagHelper : TagHelper
    {
        public string Title { get; set; } = "Submit";
        public string Class { get; set; }
        public string Id { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output?.Content.SetHtmlContent(
                $@"<button type='submit' class=""btn btn-primary {Class}"" id=""{Id}"">{Title}</button>");

            base.Process(context, output);
        }
    }
}