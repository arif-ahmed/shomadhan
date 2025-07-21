using MediatR;

namespace Somadhan.Application.Commands.Shops;
public class DeleteShopCommand : IRequest
{
    public string Id { get; set; } = default!;
}
