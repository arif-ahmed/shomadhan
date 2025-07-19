using FluentValidation;
using MediatR;
using Shomadhan.Domain.Interfaces;

namespace Shomadhan.Application.Commands.Shops;
public class CreateShopCommand : IRequest
{
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? OpeningHours { get; set; }
    public string? ClosingHours { get; set; }
    public string? OwnerName { get; set; }
}

public class CreateShopCommandHandler : IRequestHandler<CreateShopCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateShopCommandHandler( IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task Handle(CreateShopCommand request, CancellationToken cancellationToken)
    {
        var shop = new Shop
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            OpeningHours = request.OpeningHours,
            ClosingHours = request.ClosingHours,
            OwnerName = request.OwnerName
        };

        await _unitOfWork.ShopRepository.AddAsync(shop);
        await _unitOfWork.CommitAsync(cancellationToken);
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