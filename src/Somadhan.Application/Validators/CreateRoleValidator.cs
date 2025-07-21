using FluentValidation;
using Somadhan.Application.Commands.Identity;

namespace Somadhan.Application.Validators;

public class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Role name is required.")
            .MaximumLength(50).WithMessage("Role name must not exceed 50 characters.");
        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Description must not exceed 200 characters.");
        RuleFor(x => x.ShopId)
            .NotEmpty().WithMessage("Shop ID is required.");
    }
}
