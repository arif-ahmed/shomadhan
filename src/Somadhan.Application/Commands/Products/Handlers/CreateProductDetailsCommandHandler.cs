using AutoMapper;
using MediatR;
using Somadhan.Application.Dtos;
using Somadhan.Domain.Interfaces;
using Somadhan.Domain.Modules.Product;
using System.Threading;
using System.Threading.Tasks;

namespace Somadhan.Application.Commands.Products.Handlers
{
    public class CreateProductDetailsCommandHandler : IRequestHandler<CreateProductDetailsCommand, ProductDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductDetailsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDetailsDto> Handle(CreateProductDetailsCommand request, CancellationToken cancellationToken)
        {
            var productDetails = _mapper.Map<ProductDetails>(request);
            await _unitOfWork.ProductDetailsRepository.AddAsync(productDetails);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<ProductDetailsDto>(productDetails);
        }
    }
}
