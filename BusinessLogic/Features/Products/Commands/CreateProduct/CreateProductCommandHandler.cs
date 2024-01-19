using Domain.Entities;
using MediatR;
using Repository.Interfaces;

namespace BusinessLogic.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler :
        IRequestHandler<CreateProductCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(
            IUnitOfWork unitOfWork,
            IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<int> Handle(CreateProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = AssignProduct(request);
            await _productRepository.AddAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return product.ProductId;
        }

        protected internal virtual Product AssignProduct(CreateProductCommand request)
        {
            return new()
            {
                ProductId = request.ProductId,
                Name = request.Name,
                Description = request.Description,
                Status = request.Status,
                Stock = request.Stock,
                Price = request.Price,
                CreatedBy = 1,
                CreatedDate = DateTime.UtcNow
            };
        }
    }
}
