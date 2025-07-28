using MediatR;
using Somadhan.Application.Dtos;

namespace Somadhan.Application.Commands.Products;
public class UpdateBrandCommand : IRequest<BrandDto>
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
