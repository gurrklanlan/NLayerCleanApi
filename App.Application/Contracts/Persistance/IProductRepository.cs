using App.Domain.Products;

namespace App.Application.Contracts.Persistance
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        public Task<List<Product>> GetTopPriceProductAsync(int count);
    }
}
