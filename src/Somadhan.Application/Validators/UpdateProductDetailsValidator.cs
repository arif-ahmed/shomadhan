using FluentValidation;
using Somadhan.Application.Commands.Products;

namespace Somadhan.Application.Validators
{
    public class UpdateProductDetailsValidator : AbstractValidator<UpdateProductDetailsCommand>
    {
        public UpdateProductDetailsValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.StockQuantity).GreaterThanOrEqualTo(0);
        }
    }
}
