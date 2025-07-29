using MediatR;
using Somadhan.Domain.Interfaces;

namespace Somadhan.Application.Commands.Products.Handlers;
public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, bool>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBrandCommandHandler(IBrandRepository brandRepository, IUnitOfWork unitOfWork)
    {
        _brandRepository = brandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetByIdAsync(request.Id);
        if (brand == null)
        {
            return false; // Or throw a specific not found exception
        }

        await _brandRepository.DeleteAsync(request.Id);
        await _unitOfWork.CommitAsync();
        return true;
    }
}
