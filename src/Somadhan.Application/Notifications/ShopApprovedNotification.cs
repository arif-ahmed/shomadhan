using MediatR;

namespace Somadhan.Application.Notifications;
public class ShopApprovedNotification : INotification
{
    public string ShopId { get; set; } = default!;
}

public class SendEmailOnShopApprovedHandler : INotificationHandler<ShopApprovedNotification>
{
    public Task Handle(ShopApprovedNotification notification, CancellationToken cancellationToken)
    {
        // Send email logic
        return Task.CompletedTask;
    }
}

public class SendSmsOnShopApprovedHandler : INotificationHandler<ShopApprovedNotification>
{
    public Task Handle(ShopApprovedNotification notification, CancellationToken cancellationToken)
    {
        // Send SMS logic
        return Task.CompletedTask;
    }
}
