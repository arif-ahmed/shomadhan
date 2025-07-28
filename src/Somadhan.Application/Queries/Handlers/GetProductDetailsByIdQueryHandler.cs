using AutoMapper;
using MediatR;
using Somadhan.Application.Dtos;
using Somadhan.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Somadhan.Application.Queries.Handlers
{
    public class GetProductDetailsByIdQueryHandler : IRequestHandler<GetProductDetailsByIdQuery, ProductDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductDetailsByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDetailsDto> Handle(GetProductDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var productDetails = await _unitOfWork.ProductDetailsRepository.GetByIdAsync(request.Id);
            return _mapper.Map<ProductDetailsDto>(productDetails);
        }
    }
}
