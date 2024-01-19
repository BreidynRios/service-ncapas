using BusinessLogic.Commons.Models;

namespace BusinessLogic.Commons.Interfaces
{
    public interface IApiMochaServiceClient
    {
        Task<ProductDiscount> GetProductDiscount(int productId);
    }
}
