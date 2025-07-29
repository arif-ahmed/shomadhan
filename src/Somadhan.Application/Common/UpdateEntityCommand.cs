using MediatR;

namespace Somadhan.Application.Common;
public class UpdateEntityCommand<TDto> : IRequest<TDto>
{
    public string Id
    {
        get;
    }
    public TDto Dto
    {
        get;
    }

    public UpdateEntityCommand(string id, TDto dto)
    {
        Id = id;
        Dto = dto;
    }
}
