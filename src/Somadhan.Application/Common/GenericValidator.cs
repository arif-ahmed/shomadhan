using FluentValidation;

namespace Somadhan.Application.Common;
public abstract class GenericValidator<TDto> : AbstractValidator<TDto>
{
    protected GenericValidator()
    {
        DefineRules();
    }
    protected abstract void DefineRules();
}
