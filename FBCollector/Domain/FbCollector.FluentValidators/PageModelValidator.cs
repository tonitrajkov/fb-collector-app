using FbCollector.Models;
using FluentValidation;

namespace FbCollector.FluentValidators
{
    public class PageModelValidator : AbstractValidator<PageModel>
    {
        public PageModelValidator()
        {
            RuleFor(x => x.Title)
               .NotEmpty().WithMessage("REQUIRED")
               .Length(0, 250).WithMessage("MAX_LENGTH_250");

            RuleFor(x => x.Url)
               .NotEmpty().WithMessage("REQUIRED")
               .Length(0, 250).WithMessage("MAX_LENGTH_250");

            RuleFor(x => x.UrlId)
              .NotEmpty().WithMessage("REQUIRED")
              .Length(0, 100).WithMessage("MAX_LENGTH_100");

            RuleFor(x => x.Importance)
              .GreaterThan(0)
              .LessThan(4)
              .WithMessage("REQUIRED");
        }
    }
}
