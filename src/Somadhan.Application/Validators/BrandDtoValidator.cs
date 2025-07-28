using FluentValidation;

using Somadhan.Application.Common;
using Somadhan.Application.Dtos;

namespace Somadhan.Application.Validators;
public class BrandDtoValidator : GenericValidator<BrandDto>
{
    protected override void DefineRules()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(250);
    }
}
