using MediatR;
using Somadhan.Domain;

namespace Somadhan.Application.Common;
public class CreateCommand<TDto, TEntity> : IRequest where TEntity : EntityBase
{
    public TDto Dto { get; set; } = default!;
}
