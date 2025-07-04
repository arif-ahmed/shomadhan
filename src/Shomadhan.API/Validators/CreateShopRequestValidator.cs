using FluentValidation;
using Shomadhan.API.Models;

namespace Shomadhan.API.Validators;

public class CreateShopRequestValidator : AbstractValidator<CreateShopRequest>
{
    public CreateShopRequestValidator()
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
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        RuleFor(x => x.ImageUrl)
            .MaximumLength(200).WithMessage("Image URL must not exceed 200 characters.");
        RuleFor(x => x.OpeningHours)
            .MaximumLength(50).WithMessage("Opening hours must not exceed 50 characters.");
        RuleFor(x => x.ClosingHours)
            .MaximumLength(50).WithMessage("Closing hours must not exceed 50 characters.");
        RuleFor(x => x.OwnerName)
            .MaximumLength(100).WithMessage("Owner name must not exceed 100 characters.");
    }
}
