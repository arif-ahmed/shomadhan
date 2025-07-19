using MediatR;
using Microsoft.Extensions.Logging;
using Shomadhan.Application.Dtos;
using Shomadhan.Domain.Interfaces;

namespace Shomadhan.Application.Queries;
public class GetShopsQuery : IRequest<(IEnumerable<ShopDto>, int)>
{
    public string? SearchText { get; set; }
    public int Offset { get; set; } = 1;
    public int PageSize { get; set; } = 100;
}

public class GetShopsQueryHandler : IRequestHandler<GetShopsQuery, (IEnumerable<ShopDto>, int)>
{
    private readonly ILogger<GetShopsQueryHandler> _logger;
    private readonly IShopRepository _shopRepository;
    public GetShopsQueryHandler(ILogger<GetShopsQueryHandler> logger, IShopRepository shopRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _shopRepository = shopRepository ?? throw new ArgumentNullException(nameof(shopRepository));

    }
    public async Task<(IEnumerable<ShopDto>, int)> Handle(GetShopsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetShopsQuery with SearchText: {SearchText}", request.SearchText);

        var shopData = await _shopRepository.FindAsync(
            s => string.IsNullOrEmpty(request.SearchText) || s.Name.Contains(request.SearchText, StringComparison.OrdinalIgnoreCase),
            request.Offset, request.PageSize, cancellationToken);

        var shopDtos = shopData.Item1.Select(s => new ShopDto
        {
            Id = s.Id,
            Name = s.Name,
            Description = s.Description
        });

        return (shopDtos, shopData.Item2);
    }
}