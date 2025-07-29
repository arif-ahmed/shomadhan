using MediatR;
using Somadhan.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Somadhan.Application.Commands.Products.Handlers
{
    public class DeleteProductDetailsCommandHandler : IRequestHandler<DeleteProductDetailsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductDetailsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteProductDetailsCommand request, CancellationToken cancellationToken)
        {
            var productDetails = await _unitOfWork.ProductDetailsRepository.GetByIdAsync(request.Id);
            if (productDetails != null)
            {
                await _unitOfWork.ProductDetailsRepository.DeleteAsync(productDetails.Id, cancellationToken);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
