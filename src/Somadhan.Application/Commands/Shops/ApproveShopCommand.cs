
using MediatR;
using Somadhan.Application.Notifications;
using Somadhan.Domain.Interfaces;

namespace Somadhan.Application.Commands.Shops;

public class ApproveShopCommand : IRequest
{
    public string ShopId { get; set; } = default!;
    public bool IsApproved { get; set; } 
}

public class ApproveShopCommandHandler : IRequestHandler<ApproveShopCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    public ApproveShopCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    public async Task Handle(ApproveShopCommand request, CancellationToken cancellationToken)
    {
        var shop = await _unitOfWork.ShopRepository.GetByIdAsync(request.ShopId);

        if (shop == null)
        {
            ArgumentNullException.ThrowIfNull(shop);
        }

        shop.IsApproved = true;

        await _unitOfWork.ShopRepository.UpdateAsync(shop);
        await _unitOfWork.CommitAsync(cancellationToken);

        // 2. Publish notification event
        await _mediator.Publish(new ShopApprovedNotification
        {
            ShopId = request.ShopId
        }, cancellationToken);
    }
}