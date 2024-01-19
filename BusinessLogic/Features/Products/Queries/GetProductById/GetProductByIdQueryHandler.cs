using AutoMapper;
using BusinessLogic.Commons.Exceptions;
using BusinessLogic.Commons.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Repository.Interfaces;

namespace BusinessLogic.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : 
        IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IMemoryCache _cache;
        private readonly IApiMochaServiceClient _apiMochaServiceClient;

        public GetProductByIdQueryHandler(
            IMapper mapper,
            IProductRepository productRepository,
            IMemoryCache cache,
            IApiMochaServiceClient apiMochaServiceClient)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _cache = cache;
            _apiMochaServiceClient = apiMochaServiceClient;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (product is null)
                throw new NotFoundException(nameof(product), request.Id);

            var response = _mapper.Map<ProductDto>(product);
            response.StatusName = GetProductStatusName(product.Status);
            response.Discount = await GetProductDiscount(product.ProductId);
            return response;
        }

        protected internal virtual string GetProductStatusName(int status)
        {
            var productStatus = _cache.Get<List<ProductStatus>>("product-status");
            return productStatus?.FirstOrDefault(s => s.Status == status)
                    ?.StatusName ?? string.Empty;
        }

        protected internal virtual async Task<int?> GetProductDiscount(int productId)
        {
            var productDiscount = await _apiMochaServiceClient.GetProductDiscount(productId);
            return productDiscount?.Discount;
        }
    }
}
