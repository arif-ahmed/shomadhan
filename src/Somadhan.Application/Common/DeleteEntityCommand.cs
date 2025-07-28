using MediatR;
namespace Somadhan.Application.Common;
public record DeleteEntityCommand<TEntity>(string Id) : IRequest;
