using MediatR;

namespace BusinessLogic.Features.Products.Queries.GetProductById
{
    public record GetProductByIdQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}
