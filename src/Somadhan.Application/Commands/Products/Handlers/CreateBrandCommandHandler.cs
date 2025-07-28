using AutoMapper;
using MediatR;
using Somadhan.Application.Commands.Products;
using Somadhan.Application.Dtos;
using Somadhan.Domain.Interfaces;
using Somadhan.Domain.Modules.Product;

namespace Somadhan.Application.Commands.Products.Handlers;
public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, BrandDto>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBrandCommandHandler(IBrandRepository brandRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = _mapper.Map<Brand>(request);
        await _brandRepository.AddAsync(brand);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<BrandDto>(brand);
    }
}