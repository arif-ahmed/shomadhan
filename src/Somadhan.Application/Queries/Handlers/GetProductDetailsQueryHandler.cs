using AutoMapper;
using MediatR;
using Somadhan.Application.Dtos;
using Somadhan.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Somadhan.Application.Queries.Handlers
{
    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, IEnumerable<ProductDetailsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDetailsDto>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {
            var productDetails = await _unitOfWork.ProductDetailsRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDetailsDto>>(productDetails);
        }
    }
}
