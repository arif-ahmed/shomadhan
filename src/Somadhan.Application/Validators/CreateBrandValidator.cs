using FluentValidation;
using Somadhan.Application.Commands.Products;

namespace Somadhan.Application.Validators;
public class CreateBrandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Brand name is required.");
    }
}
