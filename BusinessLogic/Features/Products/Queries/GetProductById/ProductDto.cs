using AutoMapper;
using Domain.Entities;

namespace BusinessLogic.Features.Products.Queries.GetProductById
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string StatusName { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? Discount { get; set; }
        public decimal FinalPrice => Discount.HasValue ? Price * (100 - Discount.Value) / 100 : Price;
    }

    public class ProductoProfile : Profile
    {
        public ProductoProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
