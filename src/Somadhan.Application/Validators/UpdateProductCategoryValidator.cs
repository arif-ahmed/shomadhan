using FluentValidation;
using Somadhan.Application.Commands.Products;

namespace Somadhan.Application.Validators;

public class UpdateProductCategoryValidator : AbstractValidator<UpdateProductCategoryCommand>
{
    public UpdateProductCategoryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.ParentId)
            .Must(BeAValidParentCategoryId).WithMessage("Parent Category ID is not valid.");
    }

    private bool BeAValidParentCategoryId(string? parentCategoryId)
    {
        // Add logic to validate ParentCategoryId if necessary
        return true; // Assuming all IDs are valid for now
    }
}
