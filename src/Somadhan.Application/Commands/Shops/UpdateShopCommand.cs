using MediatR;
using Somadhan.Domain.Interfaces;

namespace Somadhan.Application.Commands.Shops;
public class UpdateShopCommand : IRequest
{
    public string ShopId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Address { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? OpeningHours { get; set; }
    public string? ClosingHours { get; set; }
    public string? OwnerName { get; set; }
}

public class UpdateShopCommandHandler : IRequestHandler<UpdateShopCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateShopCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(UpdateShopCommand request, CancellationToken cancellationToken)
    {
        var shop = await _unitOfWork.ShopRepository.GetByIdAsync(request.ShopId);
        if (shop == null)
        {
            throw new KeyNotFoundException($"Shop with ID {request.ShopId} not found.");
        }
        shop.Name = request.Name;
        shop.Address = request.Address;
        shop.PhoneNumber = request.PhoneNumber;
        shop.Email = request.Email;
        shop.Description = request.Description;
        shop.ImageUrl = request.ImageUrl;
        shop.OpeningHours = request.OpeningHours;
        shop.ClosingHours = request.ClosingHours;
        shop.OwnerName = request.OwnerName;
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}
