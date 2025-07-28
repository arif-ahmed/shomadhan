using MediatR;

namespace Somadhan.Application.Commands.Products;
public class DeleteBrandCommand : IRequest<bool>
{
    public string Id { get; set; } = default!;
}
