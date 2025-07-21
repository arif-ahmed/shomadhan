using FluentValidation;
using MediatR;
using Shomadhan.Domain.Interfaces;

namespace Shomadhan.Application.Commands.Shops;
public class CreateShopCommand : IRequest<string>
{
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
}

public class CreateShopCommandHandler : IRequestHandler<CreateShopCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateShopCommandHandler( IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<string> Handle(CreateShopCommand request, CancellationToken cancellationToken)
    {
        var shop = new Shop
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email
        };

        await _unitOfWork.ShopRepository.AddAsync(shop);
        await _unitOfWork.CommitAsync(cancellationToken);

        return shop.Id;
    }
}

public class CreateShopCommandValidator : AbstractValidator<CreateShopCommand>
{
    public CreateShopCommandValidator()
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
