using FluentValidation;
using Somadhan.Application.Commands.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Somadhan.Application.Validators;

public class CreateProductCategoryValidator : AbstractValidator<CreateProductCategoryCommand>
{
    public CreateProductCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        RuleFor(x => x.ParentCategoryId)
            .Must(BeAValidParentCategoryId).WithMessage("Parent Category ID is not valid.");
    }
    private bool BeAValidParentCategoryId(string? parentCategoryId)
    {
        // Add logic to validate ParentCategoryId if necessary
        return true; // Assuming all IDs are valid for now
    }
}
