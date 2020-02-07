using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Web.TagHelpers
{
    ///// <summary>
    ///// BUG: Validation doesn't work when using this
    ///// </summary>
    //[HtmlTargetElement("editor", TagStructure = TagStructure.WithoutEndTag,
    //    Attributes = ForAttributeName)]
    //public class EditorTagHelper : TagHelper
    //{
    //    private readonly IHtmlHelper _htmlHelper;

    //    private const string ForAttributeName = "asp-for";
    //    private const string TemplateAttributeName = "asp-template";

    //    [HtmlAttributeName(ForAttributeName)]
    //    public ModelExpression For { get; set; }

    //    [HtmlAttributeName(TemplateAttributeName)]
    //    public string Template { get; set; }

    //    [ViewContext]
    //    [HtmlAttributeNotBound]
    //    public ViewContext ViewContext { get; set; }

    //    public EditorTagHelper(
    //        IHtmlHelper htmlHelper)
    //    {
    //        _htmlHelper = htmlHelper;
    //    }

    //    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    //    {
    //        if (context == null)
    //            throw new ArgumentNullException(nameof(context));

    //        if (output == null)
    //            throw new ArgumentNullException(nameof(output));

    //        if (!output.Attributes.ContainsName(nameof(Template)))
    //        {
    //            output.Attributes.Add(nameof(Template), Template);
    //        }

    //        output.SuppressOutput();

    //        (_htmlHelper as IViewContextAware).Contextualize(ViewContext);

    //        output.Content.SetHtmlContent(_htmlHelper.Editor(For.Name, Template));

    //        await Task.CompletedTask;
    //    }
    //}

    /// <summary>
    /// Renders the HTML markup from an editor template for the specified model expression.
    /// </summary>
    [HtmlTargetElement("editor", Attributes = "for", TagStructure = TagStructure.WithoutEndTag)]
    public class EditorTagHelper : TagHelper
    {
        private readonly IHtmlHelper _htmlHelper;

        public EditorTagHelper(IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        /// <summary>
        /// An expression to be evaluated against the current model.
        /// </summary>
        [HtmlAttributeName("for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            ((IViewContextAware)_htmlHelper).Contextualize(ViewContext);

            output.Content.SetHtmlContent(_htmlHelper.Editor(For.Name));

            output.TagName = null;
        }
    }
}