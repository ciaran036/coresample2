using System.ComponentModel.DataAnnotations;
using Entities.Interfaces;
using FluentValidation;

namespace Entities
{
    public class SystemSettings : IEntityBase
    {
        public int Id { get; set; }

        [Display(Description = "Emails sent from system will be sent from this address.")]
        public string EmailFromAddress { get; set; }

        [Display(Description = "Name of the organisation")]
        public string CompanyName { get; set; }

        [Display(Description = "Name of the web application")]
        public string ProductName { get; set; }
    }

    public class SystemSettingsValidator : AbstractValidator<SystemSettings>
    {
        public SystemSettingsValidator()
        {
            RuleFor(x => x.EmailFromAddress).NotEmpty()
                .WithMessage("Email From Address cannot be empty");
        }
    }
}