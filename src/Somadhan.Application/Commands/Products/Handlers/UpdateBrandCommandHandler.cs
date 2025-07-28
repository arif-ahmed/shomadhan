using AutoMapper;
using MediatR;
using Somadhan.Application.Commands.Products;
using Somadhan.Application.Dtos;
using Somadhan.Domain.Interfaces;

namespace Somadhan.Application.Commands.Products.Handlers;
public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, BrandDto>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateBrandCommandHandler(IBrandRepository brandRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BrandDto> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetByIdAsync(request.Id);
        if (brand == null)
        {
            // TODO: Handle not found exception
            return null;
        }

        _mapper.Map(request, brand);
        await _brandRepository.UpdateAsync(brand);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<BrandDto>(brand);
    }
}
