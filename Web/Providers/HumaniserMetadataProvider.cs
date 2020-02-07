using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Humanizer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Web.Providers
{
    /// <summary>
    /// In the absence of a Display Name attribute on models, this will humanise the property names
    /// </summary>
    public class HumaniserMetadataProvider : IDisplayMetadataProvider
    {
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            var propertyAttributes = context.Attributes;
            var modelMetadata = context.DisplayMetadata;
            var propertyName = context.Key.Name;

            if (IsTransformRequired(propertyName, modelMetadata, propertyAttributes))
            {
                //modelMetadata.DisplayName = () => propertyName.SplitCamelCase();
                modelMetadata.DisplayName = () => propertyName.Humanize(LetterCasing.Title);
            }
        }

        private static bool IsTransformRequired(string propertyName, DisplayMetadata modelMetadata, IReadOnlyList<object> propertyAttributes)
        {
            if (!string.IsNullOrEmpty(modelMetadata.SimpleDisplayProperty))
                return false;

            if (propertyAttributes.OfType<DisplayNameAttribute>().Any())
                return false;

            if (propertyAttributes.OfType<DisplayAttribute>().Any())
            {
                var displayName = modelMetadata.DisplayName?.Invoke();
                return string.IsNullOrEmpty(displayName);
            }

            if (string.IsNullOrEmpty(propertyName))
                return false;

            return true;
        }
    }
}