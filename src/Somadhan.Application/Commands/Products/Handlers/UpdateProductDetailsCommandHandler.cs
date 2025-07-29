using AutoMapper;
using MediatR;
using Somadhan.Domain.Interfaces;
using Somadhan.Domain.Modules.Product;
using System.Threading;
using System.Threading.Tasks;

namespace Somadhan.Application.Commands.Products.Handlers
{
    public class UpdateProductDetailsCommandHandler : IRequestHandler<UpdateProductDetailsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductDetailsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateProductDetailsCommand request, CancellationToken cancellationToken)
        {
            var productDetails = await _unitOfWork.ProductDetailsRepository.GetByIdAsync(request.Id);
            if (productDetails == null)
            {
                throw new KeyNotFoundException($"Product with ID {request.Id} not found.");
            }

            await _unitOfWork.ProductDetailsRepository.UpdateAsync(productDetails, cancellationToken);

            _mapper.Map(request, productDetails);
            await _unitOfWork.CommitAsync();
        }
    }
}
