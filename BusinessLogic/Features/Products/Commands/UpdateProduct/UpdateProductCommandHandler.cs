using BusinessLogic.Commons.Exceptions;
using Domain.Entities;
using MediatR;
using Repository.Interfaces;

namespace BusinessLogic.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler :
        IRequestHandler<UpdateProductCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(
            IUnitOfWork unitOfWork,
            IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<int> Handle(UpdateProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository
                .GetByIdAsync(request.ProductId, cancellationToken);
            if (product is null)
                throw new NotFoundException(nameof(product), request.ProductId);

            AssignProduct(product, request);
            await _productRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return product.ProductId;
        }

        protected internal virtual void AssignProduct(
            Product product, UpdateProductCommand request)
        {
            product.Name = request.Name;
            product.Description = request.Description;
            product.Status = request.Status;
            product.Stock = request.Stock;
            product.Price = request.Price;
            product.UpdatedBy = 1;
            product.UpdatedDate = DateTime.UtcNow;
        }
    }
}
