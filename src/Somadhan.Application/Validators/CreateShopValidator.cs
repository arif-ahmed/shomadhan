using FluentValidation;

using Somadhan.Application.Commands.Shops;

namespace Somadhan.Application.Validators;

public class CreateShopValidator : AbstractValidator<CreateShopCommand>
{
    public CreateShopValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Shop name is required.")
            .MaximumLength(100).WithMessage("Shop name must not exceed 100 characters.");
        
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(200).WithMessage("Address must not exceed 200 characters.");
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[0-9\s-]+$").WithMessage("Phone number must be a valid format.");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid email address.");
    }
}
