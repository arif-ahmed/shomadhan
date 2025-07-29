using MediatR;
using Somadhan.Application.Dtos;

namespace Somadhan.Application.Commands.Products;
public class CreateBrandCommand : IRequest<BrandDto>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

